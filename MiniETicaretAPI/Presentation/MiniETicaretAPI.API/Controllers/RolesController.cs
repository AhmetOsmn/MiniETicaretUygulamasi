using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.Consts;
using MiniETicaretAPI.Application.CustomAttributes;
using MiniETicaretAPI.Application.Enums;
using MiniETicaretAPI.Application.Features.Commands.Role.CreateRole;
using MiniETicaretAPI.Application.Features.Queries.Role.GetRoles;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Role, Definition = "Get Roles", Action = ActionType.Reading)]
        public async Task<IActionResult> GetRolesAsync([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse getRolesQueryResponse = await _mediator.Send(getRolesQueryRequest);
            return Ok(getRolesQueryResponse);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Role, Definition = "Get Role By Id", Action = ActionType.Reading)]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse getRoleByIdQueryResponse = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(getRoleByIdQueryResponse);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Role, Definition = "Create Role", Action = ActionType.Writing)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse createRoleCommandResponse = await _mediator.Send(createRoleCommandRequest);
            return Ok(createRoleCommandResponse);
        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Role, Definition = "Update Role", Action = ActionType.Updating)]
        public async Task<IActionResult> UpdateAsync([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse updateRoleCommandResponse = await _mediator.Send(updateRoleCommandRequest);
            return Ok(updateRoleCommandResponse);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Role, Definition = "Delete Role", Action = ActionType.Deleting)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse deleteRoleCommandResponse = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(deleteRoleCommandResponse);
        }
    }
}
