using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.OrderItemOptionGroups;

namespace CofiApp.Domain.OrderItemOptions
{
    public class OrderItemOption : Entity, IAuditableEntity
    {
        public Guid OrderItemOptionGroupId { get; set; }
        public OrderItemOptionGroup OrderItemOptionGroup { get; set; }

        public Guid ProductOptionId { get; set; }
        public string ProductOptionName { get; set; }
        public decimal ProductOptionPrice { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
