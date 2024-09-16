using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.ProductOptionGroups
{
    public interface IProductOptionGroupRepository
    {
        Task<bool> AnyAsync(Guid productOptionGroupId, CancellationToken cancellationToken = default);
        Task<Maybe<ProductOptionGroup>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Insert(ProductOptionGroup productOptionGroup);
        void Update(ProductOptionGroup productOptionGroup);
        void Remove(ProductOptionGroup productOptionGroup);
    }
}
