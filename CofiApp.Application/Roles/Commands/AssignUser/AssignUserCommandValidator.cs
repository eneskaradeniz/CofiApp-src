using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Roles.Commands.AssignUser
{
    public class AssignUserCommandValidator : AbstractValidator<AssignUserCommand>
    {
        public AssignUserCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);

            RuleFor(x => x.UserId)
                .NotEmpty().WithError(ValidationErrors.Roles.UserIdIsRequired);
        }
    }
}
