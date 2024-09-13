using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.BasketItemOptionGroups;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class BasketItemOptionGroupRepository : GenericRepository<BasketItemOptionGroup>, IBasketItemOptionGroupRepository
    {
        public BasketItemOptionGroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
