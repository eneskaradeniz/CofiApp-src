using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.ProductOptions;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class ProductOptionRepository : GenericRepository<ProductOption>, IProductOptionRepository
    {
        public ProductOptionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
