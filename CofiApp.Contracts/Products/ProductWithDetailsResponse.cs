namespace CofiApp.Contracts.Products
{
    public class ProductWithDetailsResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid ProductImageFileId { get; set; }
        public List<ProductOptionGroupResponse> ProductOptionGroups { get; set; } = [];
    }
}
