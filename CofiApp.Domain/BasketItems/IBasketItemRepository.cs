using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.BasketItems
{
    public interface IBasketItemRepository
    {
        Task<Maybe<BasketItem>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Insert(BasketItem basketItem);
        void Update(BasketItem basketItem);
        void Remove(BasketItem basketItem);
        void RemoveRange(IReadOnlyCollection<BasketItem> entities);
    }
}
