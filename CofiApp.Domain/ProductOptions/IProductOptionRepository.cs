using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.ProductOptions
{
    public interface IProductOptionRepository
    {
        Task<Maybe<ProductOption>> GetByIdAsync(Guid productOptionId, CancellationToken cancellationToken = default);
        void Insert(ProductOption productOption);
        void Update(ProductOption productOption);
        void Remove(ProductOption productOption);
    }
}
