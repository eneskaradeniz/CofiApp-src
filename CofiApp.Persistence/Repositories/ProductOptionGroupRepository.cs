using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.ProductOptionGroups;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class ProductOptionGroupRepository : GenericRepository<ProductOptionGroup>, IProductOptionGroupRepository
    {
        public ProductOptionGroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
