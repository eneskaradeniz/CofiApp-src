using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductCategories;
using CofiApp.Domain.ProductOptionGroups;

namespace CofiApp.Domain.Products
{
    public class Product : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public DateTime? DeletedOnUtc { get; set; }
        public bool Deleted { get; set; }

        public ICollection<ProductOptionGroup> ProductOptionGroups { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; } = [];
    }
}
