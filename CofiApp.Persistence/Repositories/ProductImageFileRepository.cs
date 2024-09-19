using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.ProductImageFiles;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class ProductImageFileRepository : 
        GenericRepository<ProductImageFile>, IProductImageFileRepository
    {
        public ProductImageFileRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
