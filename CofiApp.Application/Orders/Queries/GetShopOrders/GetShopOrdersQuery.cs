using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Orders;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Orders.Queries.GetShopOrders
{
    public class GetShopOrdersQuery : IQuery<Maybe<PagedList<OrderResponse>>>
    {
        public GetShopOrdersQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public int Page { get; }
        public int PageSize { get; }
    }
}
