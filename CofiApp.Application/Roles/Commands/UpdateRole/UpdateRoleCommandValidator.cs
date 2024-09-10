using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);

            RuleFor(x => x.Name)
                .NotEmpty().WithError(ValidationErrors.Roles.NameIsRequired)
                .MaximumLength(50).WithError(ValidationErrors.Roles.NameIsTooLong);
        }
    }
}
