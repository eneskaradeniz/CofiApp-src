using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.Roles.Commands.AssignPermission;
using CofiApp.Application.Roles.Commands.AssignUser;
using CofiApp.Application.Roles.Commands.CreateRole;
using CofiApp.Application.Roles.Commands.RemoveRole;
using CofiApp.Application.Roles.Commands.UpdateRole;
using CofiApp.Application.Roles.Queries.GetRoles;
using CofiApp.Application.Roles.Queries.GetRolesPermissions;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class RolesController : ApiController
    {
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetRoles)]
        [HttpGet(ApiRoutes.Admin.Roles.Get)]
        [ProducesResponseType(typeof(List<RoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get() =>
            await Maybe<GetRolesQuery>
                .From(new GetRolesQuery())
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.CreateRole)]
        [HttpPost(ApiRoutes.Admin.Roles.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest createRoleRequest) =>
            await Result.Create(createRoleRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateRoleCommand(request.Name))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.UpdateRole)]
        [HttpPut(ApiRoutes.Admin.Roles.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid roleId, [FromBody] UpdateRoleRequest updateRoleRequest) =>
            await Result.Create(updateRoleRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateRoleCommand(roleId, request.Name))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.RemoveRole)]
        [HttpDelete(ApiRoutes.Admin.Roles.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid roleId) =>
            await Result.Success(new RemoveRoleCommand(roleId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.GetRolesPermissions)]
        [HttpGet(ApiRoutes.Admin.Roles.GetPermissions)]
        [ProducesResponseType(typeof(List<RoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPermissions() =>
            await Maybe<GetRolesPermissionsQuery>
                .From(new GetRolesPermissionsQuery())
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.AssignPermission)]
        [HttpPut(ApiRoutes.Admin.Roles.AssignPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignPermission(Guid roleId, int permissionId) =>
            await Result.Success(new AssignPermissionCommand(roleId, permissionId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.AssignUser)]
        [HttpPut(ApiRoutes.Admin.Roles.AssignUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignUser(Guid roleId, Guid userId) =>
            await Result.Success(new AssignUserCommand(roleId, userId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
