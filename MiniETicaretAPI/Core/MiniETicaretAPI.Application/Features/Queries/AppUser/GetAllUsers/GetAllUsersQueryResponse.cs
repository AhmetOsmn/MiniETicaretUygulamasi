using MiniETicaretAPI.Application.Dtos.User;

namespace MiniETicaretAPI.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public List<ListUser> ListUsers { get; set; }
        public int TotalCount { get; set; }
    }
}