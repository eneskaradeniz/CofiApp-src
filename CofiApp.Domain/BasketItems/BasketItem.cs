using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Products;

namespace CofiApp.Domain.BasketItems
{
    public class BasketItem : Entity, IAuditableEntity
    {
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<BasketItemOptionGroup> BasketItemOptionGroups { get; set; } = [];
    }
}
