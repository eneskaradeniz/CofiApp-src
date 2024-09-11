namespace CofiApp.Contracts.Products
{
    public class PublicProductShowcaseResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
