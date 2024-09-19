namespace CofiApp.Contracts.Products
{
    public class ProductShowcaseResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Guid ProductImageFileId { get; set; }
    }
}
