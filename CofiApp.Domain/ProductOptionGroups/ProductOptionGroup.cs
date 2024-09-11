using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductOptions;
using CofiApp.Domain.Products;

namespace CofiApp.Domain.ProductOptionGroups
{
    public class ProductOptionGroup : Entity, IAuditableEntity
    {
        public required string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<ProductOption> ProductOptions { get; set; }

        public void Update(string name, bool isRequired, bool allowMultiple)
        {
            Name = name;
            IsRequired = isRequired;
            AllowMultiple = allowMultiple;
        }
    }
}
