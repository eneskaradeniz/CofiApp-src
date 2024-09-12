using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Products;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
