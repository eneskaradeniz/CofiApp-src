using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Orders.Commands.CancelShopOrder
{
    public class CancelShopOrderCommandValidator : AbstractValidator<CancelShopOrderCommand>
    {
        public CancelShopOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithError(ValidationErrors.Orders.OrderIdIsRequired);
        }
    }
}
