using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Orders;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Orders.Queries.GetShopOrderById
{
    public class GetShopOrderByIdQueryHandler : IQueryHandler<GetShopOrderByIdQuery, Maybe<OrderResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetShopOrderByIdQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<OrderResponse>> Handle(GetShopOrderByIdQuery request, CancellationToken cancellationToken)
        {
            OrderResponse? response = await _dbContext.Set<Order>()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemOptionGroups)
                        .ThenInclude(oio => oio.OrderItemOptions)
                .AsNoTracking()
                .Where(o => o.Id == request.OrderId)
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
                        OrderItemOptionGroups = oi.OrderItemOptionGroups.Select(oio => new OrderItemOptionGroupResponse
                        {
                            Id = oio.Id,
                            OrderItemId = oio.OrderItemId,
                            ProductOptionGroupId = oio.ProductOptionGroupId,
                            ProductOptionGroupName = oio.ProductOptionGroupName,
                            OrderItemOptions = oio.OrderItemOptions.Select(oioo => new OrderItemOptionResponse
                            {
                                Id = oioo.Id,
                                OrderItemOptionGroupId = oioo.OrderItemOptionGroupId,
                                ProductOptionId = oioo.ProductOptionId,
                                ProductOptionName = oioo.ProductOptionName,
                                ProductOptionPrice = oioo.ProductOptionPrice
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);

            return response;
        }
    }
}
