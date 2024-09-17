using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Orders.Commands.ProcessShopOrder
{
    public class ProcessShopOrderCommandValidator : AbstractValidator<ProcessShopOrderCommand>
    {
        public ProcessShopOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithError(ValidationErrors.Orders.OrderIdIsRequired);
        }
    }
}
