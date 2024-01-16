namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IRoleService
    {
        (object, int) GetRoles(int page, int size);
        Task<(string id, string name)> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string id);
        Task<bool> UpdateRoleAsync(string id, string name);
    }
}
