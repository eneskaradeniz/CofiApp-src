using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Orders;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Orders.Queries.GetShopOrderById
{
    public class GetShopOrderByIdQuery : IQuery<Maybe<OrderResponse>>
    {
        public GetShopOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
