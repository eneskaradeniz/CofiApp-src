using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Orders.Commands.CancelCustomerOrder
{
    public class CancelCustomerOrderCommand : ICommand<Result>
    {
        public CancelCustomerOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
