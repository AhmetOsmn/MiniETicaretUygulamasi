using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.AppUser.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
    {
        public string ResetToken { get; set; } = null!;
        public string UserId { get; set; } = null!;

    }
}
