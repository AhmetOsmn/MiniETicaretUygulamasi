using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Abstactions.Token;
using MiniETicaretAPI.Application.Dtos;
using MiniETicaretAPI.Application.Dtos.Facebook;
using MiniETicaretAPI.Application.Exceptions;
using MiniETicaretAPI.Domain.Entities.Identity;
using System.Text.Json;

namespace MiniETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:ClientId"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:ClientSecret"]}&=client_credentials");
            FacebookAccessTokenResponseDto? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

            string userAccessTokenValidationResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");
            FacebookUserAccessTokenValidationDto? validationResponse = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidationResponse);

            if (validationResponse?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");
                FacebookUserInfoResponseDto? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponseDto>(userInfoResponse);

                UserLoginInfo userLoginInfo = new UserLoginInfo("FACEBOOK", validationResponse.Data.UserId, "FACEBOOK");

                AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
                await CreateExternalUserAsync(user, userLoginInfo, userInfo.Email, userInfo.Name, accessTokenLifeTime);
            }

            throw new Exception("Invalid external authentication");
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:ClientId"] }
            };

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            UserLoginInfo userLoginInfo = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            return await CreateExternalUserAsync(user, userLoginInfo, payload.Email, payload.Name, accessTokenLifeTime);
        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);


            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            }

            if (user == null) throw new UserNotFoundException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                var token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10 * 60);
                return token;
            }

            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenExpireDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10 * 60);
                return token;
            }
            else
                throw new UserNotFoundException();

        }

        private async Task<Token> CreateExternalUserAsync(AppUser user, UserLoginInfo userLoginInfo, string email, string name, int accessTokenLifeTime)
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    //create new user object with payload
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    var createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, userLoginInfo);

                var token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10 * 60);
                return token;
            }
            throw new Exception("Invalid external authentication");
        }
    }
}
