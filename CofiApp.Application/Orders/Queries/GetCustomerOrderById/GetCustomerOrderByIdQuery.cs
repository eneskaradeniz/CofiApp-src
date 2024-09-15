using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Orders;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Orders.Queries.GetCustomerOrderById
{
    public class GetCustomerOrderByIdQuery : IQuery<Maybe<OrderResponse>>
    {
        public GetCustomerOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
