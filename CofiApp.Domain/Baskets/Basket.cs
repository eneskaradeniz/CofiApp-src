using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Orders;
using CofiApp.Domain.Users;

namespace CofiApp.Domain.Baskets
{
    public class Basket : Entity, IAuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public BasketStatus Status { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];

        public decimal TotalPrice => BasketItems.Sum(x => x.TotalPrice);
    }
}
