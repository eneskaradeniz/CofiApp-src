using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithError(ValidationErrors.Authentication.TokenIsRequired);

            RuleFor(x => x.Password)
                .NotEmpty().WithError(ValidationErrors.Authentication.PasswordIsRequired)
                .MinimumLength(6).WithError(ValidationErrors.Authentication.PasswordIsTooShort);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithError(ValidationErrors.Authentication.ConfirmPasswordIsRequired)
                .Equal(x => x.Password).WithError(ValidationErrors.Authentication.ConfirmPasswordIsDifferent);
        }
    }
}
