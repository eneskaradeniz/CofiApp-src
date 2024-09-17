using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Orders;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Orders.Queries.GetShopOrders
{
    public class GetShopOrdersQueryHandler : IQueryHandler<GetShopOrdersQuery, Maybe<PagedList<OrderResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetShopOrdersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<PagedList<OrderResponse>>> Handle(GetShopOrdersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<OrderResponse> query = _dbContext.Set<Order>()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemOptionGroups)
                        .ThenInclude(oig => oig.OrderItemOptions)
                .OrderByDescending(o => o.CreatedOnUtc)
                .Select(o => new OrderResponse
                {
                    Id = o.Id,
                    BasketId = o.BasketId,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    CreatedOnUtc = o.CreatedOnUtc,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemResponse
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        ProductId = oi.ProductId,
                        ProductName = oi.ProductName,
                        ProductPrice = oi.ProductPrice,
                        Quantity = oi.Quantity,
                        TotalPrice = oi.TotalPrice,
                        OrderItemOptionGroups = oi.OrderItemOptionGroups.Select(oig => new OrderItemOptionGroupResponse
                        {
                            Id = oig.Id,
                            OrderItemId = oig.OrderItemId,
                            ProductOptionGroupId = oig.ProductOptionGroupId,
                            ProductOptionGroupName = oig.ProductOptionGroupName,
                            OrderItemOptions = oig.OrderItemOptions.Select(oi => new OrderItemOptionResponse
                            {
                                Id = oi.Id,
                                OrderItemOptionGroupId = oi.OrderItemOptionGroupId,
                                ProductOptionId = oi.ProductOptionId,
                                ProductOptionName = oi.ProductOptionName,
                                ProductOptionPrice = oi.ProductOptionPrice
                            }).ToList()
                        }).ToList()
                    }).ToList()
                });

            var totalCount = await query.CountAsync(cancellationToken);

            OrderResponse[] responseArray = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PagedList<OrderResponse>(responseArray, request.Page, request.PageSize, totalCount);
        }
    }
}
