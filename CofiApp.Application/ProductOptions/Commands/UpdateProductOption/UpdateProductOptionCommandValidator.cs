using CofiApp.Application.Core.Errors;
using FluentValidation;

namespace CofiApp.Application.ProductOptions.Commands.UpdateProductOption
{
    public class UpdateProductOptionCommandValidator : AbstractValidator<UpdateProductOptionCommand>
    {
        public UpdateProductOptionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidationErrors.ProductOptions.IdIsRequired);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrors.ProductOptions.NameIsRequired)
                .MaximumLength(100).WithMessage(ValidationErrors.ProductOptions.NameIsTooLong);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ValidationErrors.ProductOptions.PriceIsRequired);
        }
    }
}
