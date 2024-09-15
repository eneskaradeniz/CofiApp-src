using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Orders.Commands.UpdateShopOrderStatus
{
    public class UpdateShopOrderStatusCommandValidator : AbstractValidator<UpdateShopOrderStatusCommand>
    {
        public UpdateShopOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithError(ValidationErrors.Orders.OrderIdIsRequired);

            RuleFor(x => x.Status)
                .NotEmpty().WithError(ValidationErrors.Orders.OrderStatusIsRequired)
                .IsInEnum().WithError(ValidationErrors.Orders.OrderStatusIsInvalid);
        }
    }
}
