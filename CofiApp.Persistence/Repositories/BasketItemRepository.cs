using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.BasketItems;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class BasketItemRepository : GenericRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
