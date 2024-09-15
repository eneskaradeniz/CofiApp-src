using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.OrderItemOptionGroups;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class OrderItemOptionGroupRepository : 
        GenericRepository<OrderItemOptionGroup>, IOrderItemOptionGroupRepository
    {
        public OrderItemOptionGroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
