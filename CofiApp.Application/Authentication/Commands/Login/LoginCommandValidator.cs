using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithError(ValidationErrors.Authentication.EmailIsRequired)
                .EmailAddress().WithError(ValidationErrors.Authentication.EmailIsInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithError(ValidationErrors.Authentication.PasswordIsRequired);
        }
    }
}
