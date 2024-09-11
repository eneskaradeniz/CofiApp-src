using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Domain.Products;

namespace CofiApp.Domain.MenuCategories
{
    public class MenuCategory : Entity, IAuditableEntity
    {
        public required string Name { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<ProductMenuCategory> ProductMenuCategories { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
    }
}
