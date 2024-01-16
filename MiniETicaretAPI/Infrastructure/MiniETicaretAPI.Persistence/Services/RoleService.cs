using Microsoft.AspNetCore.Identity;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<(string id, string name)> GetRoleByIdAsync(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public (object, int) GetRoles(int page, int size)
        {
            IQueryable<AppRole> query = _roleManager.Roles;
            return (query.Skip(page * size).Take(size).Select(role => new { role.Id, role.Name }), query.Count());
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
            return result.Succeeded;
        }
    }
}
