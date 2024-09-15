using CofiApp.Contracts.Common;
using CofiApp.Contracts.Products;

namespace CofiApp.Contracts.MenuCategories
{
    public class MenuCategoryWithProductsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PagedList<ProductShowcaseResponse> Products { get; set; }
    }
}
