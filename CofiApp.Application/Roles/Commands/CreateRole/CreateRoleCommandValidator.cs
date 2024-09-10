using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithError(ValidationErrors.Roles.NameIsRequired)
                .MaximumLength(50).WithError(ValidationErrors.Roles.NameIsTooLong);
        }
    }
}
