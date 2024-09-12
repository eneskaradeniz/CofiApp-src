using CofiApp.Application.Core.Errors;
using FluentValidation;

namespace CofiApp.Application.ProductOptions.Commands.CreateProductOption
{
    public class CreateProductOptionCommandValidator : AbstractValidator<CreateProductOptionCommand>
    {
        public CreateProductOptionCommandValidator()
        {
            RuleFor(x => x.ProductOptionGroupId)
                .NotEmpty().WithMessage(ValidationErrors.ProductOptions.ProductOptionGroupIdIsRequired);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrors.ProductOptions.NameIsRequired)
                .MaximumLength(100).WithMessage(ValidationErrors.ProductOptions.NameIsTooLong);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ValidationErrors.ProductOptions.PriceIsRequired);
        }
    }
}
