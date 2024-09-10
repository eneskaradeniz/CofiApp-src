using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.ProductCategories;

namespace CofiApp.Domain.MenuCategories
{
    public class MenuCategory : Entity, IAuditableEntity
    {
        public required string Name { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = [];
    }
}
