using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.Users.Commands.RemoveUser;
using CofiApp.Application.Users.Commands.UpdateMyProfile;
using CofiApp.Application.Users.Commands.UpdateUser;
using CofiApp.Application.Users.Queries.GetMyProfile;
using CofiApp.Application.Users.Queries.GetUserById;
using CofiApp.Application.Users.Queries.GetUsers;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetUserById)]
        [HttpGet(ApiRoutes.Admin.Users.GetById)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid userId) =>
            await Maybe<GetUserByIdQuery>
                .From(new GetUserByIdQuery(userId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.GetUsers)]
        [HttpGet(ApiRoutes.Admin.Users.Get)]
        [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int page, int pageSize) =>
            await Maybe<GetUsersQuery>
                .From(new GetUsersQuery(page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.UpdateUser)]
        [HttpPut(ApiRoutes.Admin.Users.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateUserRequest updateUserRequest) =>
            await Result.Create(updateUserRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateUserCommand(userId, request.FirstName, request.LastName))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.RemoveUser)]
        [HttpDelete(ApiRoutes.Admin.Users.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid userId) =>
            await Result.Success(new RemoveUserCommand(userId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpGet(ApiRoutes.Customer.Users.GetMyProfile)]
        [ProducesResponseType(typeof(MyProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyProfile() =>
            await Result.Create(new GetMyProfileQuery(), DomainErrors.General.UnProcessableRequest)
                .Bind(query => Mediator.Send(query))
                .Match(Ok, BadRequest);

        [HttpPut(ApiRoutes.Customer.Users.UpdateMyProfile)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMyProfileRequest updateMyProfileRequest) =>
            await Result.Create(updateMyProfileRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateMyProfileCommand(request.FirstName, request.LastName))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
