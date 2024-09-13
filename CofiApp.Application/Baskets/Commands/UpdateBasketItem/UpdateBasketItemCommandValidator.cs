using CofiApp.Application.Core.Errors;
using FluentValidation;

namespace CofiApp.Application.Baskets.Commands.UpdateBasketItem
{
    public class UpdateBasketItemCommandValidator : AbstractValidator<UpdateBasketItemCommand>
    {
        public UpdateBasketItemCommandValidator()
        {
            RuleFor(x => x.BasketItemId)
                .NotEmpty().WithMessage(ValidationErrors.Baskets.BasketItemIdIsRequired);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage(ValidationErrors.Baskets.QuantityMustBeGreaterThanZero);
        }
    }
}
