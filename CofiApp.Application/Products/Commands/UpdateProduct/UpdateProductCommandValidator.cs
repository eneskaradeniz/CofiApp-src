using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithError(ValidationErrors.Products.IdIsRequired);

            RuleFor(x => x.Name)
                .NotEmpty().WithError(ValidationErrors.Products.NameIsRequired)
                .MaximumLength(100).WithError(ValidationErrors.Products.NameIsTooLong);

            RuleFor(x => x.Description)
                .MaximumLength(500).WithError(ValidationErrors.Products.DescriptionIsTooLong);

            RuleFor(x => x.Price)
                .NotEmpty().WithError(ValidationErrors.Products.PriceIsRequired)
                .GreaterThan(0).WithError(ValidationErrors.Products.PriceIsInvalid);
        }
    }
}
