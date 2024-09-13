using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Baskets
{
    public interface IBasketRepository
    {
        Task<Maybe<Basket>> GetActiveBasketByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        void Insert(Basket basket);
    }
}
