using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Maybe<Order>> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        void Insert(Order order);
        void Update(Order order);
    }
}
