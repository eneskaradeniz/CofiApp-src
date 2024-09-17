using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Orders.Commands.CancelShopOrder
{
    public class CancelShopOrderCommand : ICommand<Result>
    {
        public CancelShopOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
