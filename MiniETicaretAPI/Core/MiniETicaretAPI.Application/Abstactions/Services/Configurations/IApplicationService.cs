using MiniETicaretAPI.Application.Dtos.Configuration;

namespace MiniETicaretAPI.Application.Abstactions.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
