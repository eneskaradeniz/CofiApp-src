using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.OrderItems;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
