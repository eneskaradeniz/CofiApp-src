namespace CofiApp.Contracts.ProductOptions
{
    public class ProductOptionResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
