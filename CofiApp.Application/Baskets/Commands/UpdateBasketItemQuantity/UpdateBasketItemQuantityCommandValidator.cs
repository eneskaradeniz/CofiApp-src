using CofiApp.Application.Core.Errors;
using FluentValidation;

namespace CofiApp.Application.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandValidator : AbstractValidator<UpdateBasketItemQuantityCommand>
    {
        public UpdateBasketItemQuantityCommandValidator()
        {
            RuleFor(x => x.BasketItemId)
                .NotEmpty().WithMessage(ValidationErrors.Baskets.BasketItemIdIsRequired);

            RuleFor(x => x.IsIncrease)
                .NotNull().WithMessage(ValidationErrors.Baskets.IsIncreaseIsRequired);
        }
    }
}
