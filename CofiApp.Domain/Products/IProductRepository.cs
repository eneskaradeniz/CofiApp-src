using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Products
{
    public interface IProductRepository
    {
        Task<bool> AnyAsync(Guid productId);
        Task<Maybe<Product>> GetByIdAsync(Guid productId);
        void Insert(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
