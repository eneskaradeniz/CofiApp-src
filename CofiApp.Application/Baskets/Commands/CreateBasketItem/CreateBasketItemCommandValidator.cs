using CofiApp.Application.Core.Errors;
using FluentValidation;

namespace CofiApp.Application.Baskets.Commands.CreateBasketItem
{
    public class CreateBasketItemCommandValidator : AbstractValidator<CreateBasketItemCommand>
    {
        public CreateBasketItemCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(ValidationErrors.Baskets.ProductIdIsRequired);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage(ValidationErrors.Baskets.QuantityMustBeGreaterThanZero);
        }
    }
}
