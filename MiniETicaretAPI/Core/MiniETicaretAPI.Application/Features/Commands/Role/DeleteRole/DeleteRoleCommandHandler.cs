using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Commands.Role.CreateRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(request.Id);
            return new()
            {
                Succeeded = result
            };
        }
    }

}
