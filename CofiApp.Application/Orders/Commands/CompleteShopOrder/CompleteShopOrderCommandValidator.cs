using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Orders.Commands.CompleteShopOrder
{
    public class CompleteShopOrderCommandValidator : AbstractValidator<CompleteShopOrderCommand>
    {
        public CompleteShopOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithError(ValidationErrors.Orders.OrderIdIsRequired);
        }
    }
}
