namespace CofiApp.Contracts.ProductOptionGroups
{
    public class ProductOptionGroupsWithOptionsResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public IEnumerable<ProductOptionResponse> Options { get; set; } = [];
    }
}
