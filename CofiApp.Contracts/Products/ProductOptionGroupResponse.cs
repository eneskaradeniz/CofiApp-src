using CofiApp.Contracts.ProductOptions;

namespace CofiApp.Contracts.Products
{
    public class ProductOptionGroupResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }

        public List<ProductOptionResponse> Options { get; set; } = [];
    }
}
