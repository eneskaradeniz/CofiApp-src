using CofiApp.Domain.BasketItemOptions;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductOptionGroups;

namespace CofiApp.Domain.BasketItemOptionGroups
{
    public class BasketItemOptionGroup : Entity, IAuditableEntity
    {
        public Guid BasketItemId { get; set; }
        public BasketItem BasketItem { get; set; }

        public Guid ProductOptionGroupId { get; set; }
        public ProductOptionGroup ProductOptionGroup { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<BasketItemOption> BasketItemOptions { get; set; } = [];
    }
}
