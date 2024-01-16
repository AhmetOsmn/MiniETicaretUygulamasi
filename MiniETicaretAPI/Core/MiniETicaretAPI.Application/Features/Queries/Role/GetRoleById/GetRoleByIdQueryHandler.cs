using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var (id, name) = await _roleService.GetRoleByIdAsync(request.Id);
            return new()
            {
                Id = id,
                Name = name
            };
        }
    }

}
