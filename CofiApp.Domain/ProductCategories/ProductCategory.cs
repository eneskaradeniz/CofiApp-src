using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.Products;

namespace CofiApp.Domain.ProductCategories
{
    public class ProductCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }
    }
}
