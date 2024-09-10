using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Roles.Commands.AssignPermission
{
    public class AssignPermissionCommandValidator : AbstractValidator<AssignPermissionCommand>
    {
        public AssignPermissionCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);

            RuleFor(x => x.PermissionId)
                .NotEmpty().WithError(ValidationErrors.Roles.PermissionIdIsRequired);
        }
    }
}
