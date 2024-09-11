using CofiApp.Contracts.Common;
using CofiApp.Contracts.Products;

namespace CofiApp.Contracts.MenuCategories
{
    public class PublicMenuCategoryWithProductsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PagedList<PublicProductShowcaseResponse> Products { get; set; }
    }
}
