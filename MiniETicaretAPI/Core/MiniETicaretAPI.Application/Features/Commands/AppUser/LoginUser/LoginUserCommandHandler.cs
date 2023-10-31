using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            return new()
            {
                Token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 15 * 60)
            };
        }
    }
}

