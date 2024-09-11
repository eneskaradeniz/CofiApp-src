namespace CofiApp.Contracts.Products
{
    public class PublicProductOptionGroupResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }

        public List<PublicProductOptionResponse> Options { get; set; } = [];
    }
}
