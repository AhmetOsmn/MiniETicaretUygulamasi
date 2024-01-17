using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.CustomAttributes;
using MiniETicaretAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using MiniETicaretAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoints;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("get-roles-to-endpoint")]        
        public async Task<IActionResult> GetRolesToEndpoint([FromBody] GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
            GetRolesToEndpointQueryResponse getRolesEndpointQueryResponse = await _mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(getRolesEndpointQueryResponse);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            AssignRoleEndpointCommandResponse assignRoleEndpointCommandResponse = await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(assignRoleEndpointCommandResponse);
        }
    }
}
