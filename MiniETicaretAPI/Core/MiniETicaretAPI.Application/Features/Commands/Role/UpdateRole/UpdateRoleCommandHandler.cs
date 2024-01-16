using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Commands.Role.CreateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _roleService.UpdateRoleAsync(request.Id, request.Name) ;
            return new()
            {
                Succeeded = result
            };
        }
    }

}
