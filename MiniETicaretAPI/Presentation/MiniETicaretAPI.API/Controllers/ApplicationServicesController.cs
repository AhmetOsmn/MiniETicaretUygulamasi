using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.Abstactions.Services.Configurations;
using MiniETicaretAPI.Application.Consts;
using MiniETicaretAPI.Application.CustomAttributes;
using MiniETicaretAPI.Application.Enums;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ApplicationService, Definition = "Get Application Services", Action = ActionType.Reading)]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            return Ok(_applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program)));
        }
    }
}
