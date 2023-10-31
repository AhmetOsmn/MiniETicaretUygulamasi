using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Dtos.Token token = await _authService.GoogleLoginAsync(request.IdToken, 15 * 60);

            return new()
            {
                Token = token
            };
        }
    }
}
