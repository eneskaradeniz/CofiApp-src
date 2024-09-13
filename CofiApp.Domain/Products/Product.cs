using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Domain.ProductOptionGroups;

namespace CofiApp.Domain.Products
{
    public class Product : Entity, ISoftDeletableEntity, IAuditableEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public DateTime? DeletedOnUtc { get; set; }
        public bool Deleted { get; set; }

        public ICollection<ProductOptionGroup> ProductOptionGroups { get; set; } = [];

        public ICollection<MenuCategory> MenuCategories { get; set; } = [];
        public ICollection<ProductMenuCategory> ProductMenuCategories { get; set; } = [];

        public ICollection<BasketItem> BasketItems { get; set; } = [];

        public void Update(string name, string? description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
