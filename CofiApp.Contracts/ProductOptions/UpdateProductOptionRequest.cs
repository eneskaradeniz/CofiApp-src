namespace CofiApp.Contracts.ProductOptions
{
    public class UpdateProductOptionRequest
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
