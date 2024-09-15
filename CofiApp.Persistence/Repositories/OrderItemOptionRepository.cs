using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.OrderItemOptions;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class OrderItemOptionRepository :
        GenericRepository<OrderItemOption>, IOrderItemOptionRepository
    {
        public OrderItemOptionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
