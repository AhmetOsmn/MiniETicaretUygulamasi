using MediatR;

namespace MiniETicaretAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRoleByIdQueryRequest : IRequest<GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
