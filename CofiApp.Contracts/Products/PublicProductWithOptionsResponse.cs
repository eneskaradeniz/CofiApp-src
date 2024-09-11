namespace CofiApp.Contracts.Products
{
    public class PublicProductWithOptionsResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<PublicProductOptionGroupResponse> ProductOptionGroups { get; set; } = [];
    }
}
