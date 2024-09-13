using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductOptions;

namespace CofiApp.Domain.BasketItemOptions
{
    public class BasketItemOption : Entity, IAuditableEntity
    {
        public Guid BasketItemOptionGroupId { get; set; }
        public BasketItemOptionGroup BasketItemOptionGroup { get; set; }

        public Guid ProductOptionId { get; set; }
        public ProductOption ProductOption { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
