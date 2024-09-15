using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Orders;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
