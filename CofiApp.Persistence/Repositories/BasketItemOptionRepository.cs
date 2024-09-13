using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.BasketItemOptions;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class BasketItemOptionRepository : GenericRepository<BasketItemOption>, IBasketItemOptionRepository
    {
        public BasketItemOptionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
