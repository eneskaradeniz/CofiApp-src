using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Maybe<Basket>> GetActiveBasketByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            await DbContext.Set<Basket>()
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.BasketItemOptionGroups)
                        .ThenInclude(biog => biog.ProductOptionGroup)
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.BasketItemOptionGroups)
                        .ThenInclude(biog => biog.BasketItemOptions)
                            .ThenInclude(bio => bio.ProductOption)
                .Where(b => b.UserId == userId && b.Status == BasketStatus.Active)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
