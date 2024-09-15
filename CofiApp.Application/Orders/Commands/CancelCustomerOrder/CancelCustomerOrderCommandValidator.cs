using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Orders.Commands.CancelCustomerOrder
{
    public class CancelCustomerOrderCommandValidator : AbstractValidator<CancelCustomerOrderCommand>
    {
        public CancelCustomerOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithError(ValidationErrors.Orders.OrderIdIsRequired);
        }
    }
}
