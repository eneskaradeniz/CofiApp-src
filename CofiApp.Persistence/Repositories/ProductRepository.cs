using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ProductExistsAsync(Guid productId) =>
             await DbContext.Set<Product>()
                .AsNoTracking()
                .AsQueryable()
                .AnyAsync(x => x.Id == productId);
    }
}
