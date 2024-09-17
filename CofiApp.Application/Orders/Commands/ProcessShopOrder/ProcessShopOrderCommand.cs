using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Orders.Commands.ProcessShopOrder
{
    public class ProcessShopOrderCommand : ICommand<Result>
    {
        public ProcessShopOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
