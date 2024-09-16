using CofiApp.Contracts.ProductOptions;

namespace CofiApp.Contracts.Baskets
{
    public class CreateBasketItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public List<ProductOptionDto> ProductOptions { get; set; } = [];
    }
}
