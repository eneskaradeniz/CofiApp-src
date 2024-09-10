using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithError(ValidationErrors.Authentication.FirstNameIsRequired);

            RuleFor(x => x.LastName)
                .NotEmpty().WithError(ValidationErrors.Authentication.LastNameIsRequired);

            RuleFor(x => x.Email)
                .NotEmpty().WithError(ValidationErrors.Authentication.EmailIsRequired)
                .EmailAddress().WithError(ValidationErrors.Authentication.EmailIsInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithError(ValidationErrors.Authentication.PasswordIsRequired)
                .MinimumLength(6).WithError(ValidationErrors.Authentication.PasswordIsTooShort);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithError(ValidationErrors.Authentication.ConfirmPasswordIsRequired)
                .Equal(x => x.Password).WithError(ValidationErrors.Authentication.ConfirmPasswordIsDifferent);
        }
    }
}
