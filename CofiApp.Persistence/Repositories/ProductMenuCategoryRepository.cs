using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.ProductMenuCategories;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal class ProductMenuCategoryRepository : 
        GenericRepository<ProductMenuCategory>, IProductMenuCategoryRepository
    {
        public ProductMenuCategoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductMenuCategory>> GetByProductIdAsync(Guid productId) =>
            await DbContext.Set<ProductMenuCategory>()
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .ToListAsync();
    }
}
