using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var (roles, count) = _roleService.GetRoles(request.Page, request.Size);
            return new()
            {
                Roles = roles,
                TotalCount = count
            };
        }
    }           
}
