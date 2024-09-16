using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Products
{
    public interface IProductRepository
    {
        Task<bool> AnyAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Maybe<Product>> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        void Insert(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
