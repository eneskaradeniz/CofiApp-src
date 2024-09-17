using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Orders.Commands.CompleteShopOrder
{
    public class CompleteShopOrderCommand : ICommand<Result>
    {
        public CompleteShopOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
