using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductOptions;

namespace CofiApp.Domain.BasketItemOptions
{
    public class BasketItemOption : Entity, IAuditableEntity
    {
        public Guid BasketItemId { get; set; }
        public BasketItem BasketItem { get; set; }

        public Guid ProductOptionId { get; set; }
        public ProductOption ProductOption { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
