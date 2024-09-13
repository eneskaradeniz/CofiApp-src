namespace CofiApp.Contracts.Products
{
    public class ProductForBasketItemResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
