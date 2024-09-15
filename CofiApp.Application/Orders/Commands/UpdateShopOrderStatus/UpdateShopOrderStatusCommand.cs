using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;

namespace CofiApp.Application.Orders.Commands.UpdateShopOrderStatus
{
    public class UpdateShopOrderStatusCommand : ICommand<Result>
    {
        public UpdateShopOrderStatusCommand(Guid orderId, OrderStatus status)
        {
            OrderId = orderId;
            Status = status;
        }

        public Guid OrderId { get; }
        public OrderStatus Status { get; }
    }
}
