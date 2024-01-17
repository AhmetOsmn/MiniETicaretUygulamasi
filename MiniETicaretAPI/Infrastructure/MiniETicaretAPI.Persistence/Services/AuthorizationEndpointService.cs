using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Abstactions.Services.Configurations;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Domain.Entities.Identity;
using System.Data;

namespace MiniETicaretAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;

        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriteRepository _endpointWriteRepository;

        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;

        private readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService,
                                            IEndpointReadRepository endpointReadRepository,
                                            IEndpointWriteRepository endpointWriteRepository,
                                            IMenuWriteRepository menuWriteRepository,
                                            IMenuReadRepository menuReadRepository,
                                            RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuWriteRepository = menuWriteRepository;
            _menuReadRepository = menuReadRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string code, string menu, Type type)
        {
            Menu selectedMenu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);

            if (selectedMenu == null)
            {
                selectedMenu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriteRepository.AddAsync(selectedMenu);

                await _endpointWriteRepository.SaveAsync();
            }


            Endpoint? selectedEndpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (selectedEndpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
                                                    .FirstOrDefault(m => m.Name == menu)
                                                    ?.Actions.FirstOrDefault(e => e.Code == code);

                selectedEndpoint = new()
                {
                    Code = code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = selectedMenu,
                };

                await _endpointWriteRepository.AddAsync(selectedEndpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            selectedEndpoint.Roles.Clear();

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
            {
                selectedEndpoint.Roles.Add(role);
            }

            await _endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpoint(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table
                                            .Include(e => e.Roles)
                                            .Include(e => e.Menu)
                                            .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (endpoint == null)
                return new();

            return endpoint.Roles.Select(r => r.Name).ToList();
        }
    }
}
