using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.Authentication.Commands.EmailConfirmation;
using CofiApp.Application.Authentication.Commands.ForgotPassword;
using CofiApp.Application.Authentication.Commands.Login;
using CofiApp.Application.Authentication.Commands.RefreshToken;
using CofiApp.Application.Authentication.Commands.Register;
using CofiApp.Application.Authentication.Commands.ResetPassword;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) =>
            await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new LoginCommand(request.Email, request.Password))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.Authentication.Register)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) =>
            await Result.Create(registerRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new RegisterCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password,
                    request.ConfirmPassword))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.Authentication.RefreshToken)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest) =>
            await Result.Create(refreshTokenRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new RefreshTokenCommand(request.RefreshToken))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpGet(ApiRoutes.Authentication.EmailConfirmation)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string token) =>
            await Result.Create(token, DomainErrors.General.UnProcessableRequest)
                .Map(request => new EmailConfirmationCommand(request))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.Authentication.ForgotPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email) =>
            await Result.Create(email, DomainErrors.General.UnProcessableRequest)
                .Map(request => new ForgotPasswordCommand(request))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.Authentication.ResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest) =>
            await Result.Create(resetPasswordRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new ResetPasswordCommand(request.Token, request.Password, request.ConfirmPassword))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
