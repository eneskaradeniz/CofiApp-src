namespace CofiApp.Contracts.ProductOptions
{
    public class CreateProductOptionRequest
    {
        public required string Name { get; set; }

        public decimal Price { get; set; }
    }
}
