using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.OrderItemOptionGroups;
using CofiApp.Domain.Orders;

namespace CofiApp.Domain.OrderItems
{
    public class OrderItem : Entity, IAuditableEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<OrderItemOptionGroup> OrderItemOptionGroups { get; set; } = [];
    }
}
