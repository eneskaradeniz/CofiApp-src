using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.ProductImageFiles
{
    public interface IProductImageFileRepository
    {
        Task<Maybe<ProductImageFile>> GetByIdAsync(Guid productImageFileId, CancellationToken cancellationToken = default);
        void Insert(ProductImageFile productImageFile);
        void Remove(ProductImageFile productImageFile);
    }
}
