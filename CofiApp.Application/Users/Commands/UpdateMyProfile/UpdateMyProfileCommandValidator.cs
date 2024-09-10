using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Users.Commands.UpdateMyProfile
{
    public class UpdateMyProfileCommandValidator : AbstractValidator<UpdateMyProfileCommand>
    {
        public UpdateMyProfileCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithError(ValidationErrors.Users.FirstNameIsRequired);

            RuleFor(x => x.LastName)
                .NotEmpty().WithError(ValidationErrors.Users.LastNameIsRequired);
        }
    }
}
