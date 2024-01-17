namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IAuthorizationEndpointService
    {
        Task AssignRoleEndpointAsync(string[] roles, string code, string menu, Type type);
        Task<List<string>> GetRolesToEndpoint(string code, string manu);
    }
}
