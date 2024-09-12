using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.ProductOptionGroups
{
    public interface IProductOptionGroupRepository
    {
        Task<bool> AnyAsync(Guid productOptionGroupId);
        Task<Maybe<ProductOptionGroup>> GetByIdAsync(Guid id);
        void Insert(ProductOptionGroup productOptionGroup);
        void Update(ProductOptionGroup productOptionGroup);
        void Remove(ProductOptionGroup productOptionGroup);
    }
}
