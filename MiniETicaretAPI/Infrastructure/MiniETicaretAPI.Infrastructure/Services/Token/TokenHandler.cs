using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniETicaretAPI.Application.Abstactions.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace MiniETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.Dtos.Token CreateAccessToken(int second)
        {
            Application.Dtos.Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddSeconds(second);

            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                expires: token.Expiration
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(number);

            return Convert.ToBase64String(number);

        }
    }
}
