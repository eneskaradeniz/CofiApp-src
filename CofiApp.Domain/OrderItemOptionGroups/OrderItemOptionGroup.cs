using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.OrderItemOptions;
using CofiApp.Domain.OrderItems;

namespace CofiApp.Domain.OrderItemOptionGroups
{
    public class OrderItemOptionGroup : Entity, IAuditableEntity
    {
        public Guid OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }

        public Guid ProductOptionGroupId { get; set; }
        public required string ProductOptionGroupName { get; set; }
        public bool ProductOptionGroupIsRequired { get; set; }
        public bool ProductOptionGroupAllowMultiple { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<OrderItemOption> OrderItemOptions { get; set; } = [];
    }
}
