using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.OrderItems;

namespace CofiApp.Domain.OrderItemOptions
{
    public class OrderItemOption : Entity, IAuditableEntity
    {
        public Guid OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        
        public string ProductOptionName { get; set; }
        public decimal ProductOptionPrice { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
