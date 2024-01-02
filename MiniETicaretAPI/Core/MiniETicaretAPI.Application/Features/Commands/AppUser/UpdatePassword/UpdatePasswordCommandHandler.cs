using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Exceptions;

namespace MiniETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if(!request.NewPassword.Equals(request.PasswordConfirm))
                throw new PasswordUpdateFailedException("Lütfen şifreyi doğrulayınız.");

            await _userService.UpdatePasswordAsync(request.UserId, request.NewPassword, request.ResetToken);

            return new();
        }
    }
}
