using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace CofiApp.Domain.ProductMenuCategories
{
    public class ProductMenuCategory : Entity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }

        [NotMapped]
        public new Guid Id { get; private set; }
    }
}
