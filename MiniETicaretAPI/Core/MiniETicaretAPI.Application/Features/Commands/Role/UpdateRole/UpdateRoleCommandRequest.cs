using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Role.CreateRole
{
    public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
