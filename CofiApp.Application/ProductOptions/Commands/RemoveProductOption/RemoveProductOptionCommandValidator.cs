using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.ProductOptions.Commands.RemoveProductOption
{
    public class RemoveProductOptionCommandValidator : AbstractValidator<RemoveProductOptionCommand>
    {
        public RemoveProductOptionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithError(ValidationErrors.ProductOptions.IdIsRequired);
        }
    }
}
