using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Enums;
using CofiApp.Domain.OrderItems;
using CofiApp.Domain.Users;

namespace CofiApp.Domain.Orders
{
    public class Order : Entity, IAuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }

        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
