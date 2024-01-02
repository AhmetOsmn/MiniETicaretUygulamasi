using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
    {
        public string UserId { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ResetToken { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
    }
}
