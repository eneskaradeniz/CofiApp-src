using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductOptionGroups;

namespace CofiApp.Domain.ProductOptions
{
    public class ProductOption : Entity, IAuditableEntity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public Guid ProductOptionGroupId { get; set; }
        public ProductOptionGroup ProductOptionGroup { get; set; }

        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
