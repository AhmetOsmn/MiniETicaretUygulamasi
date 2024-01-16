using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Role.CreateRole
{
    public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}
