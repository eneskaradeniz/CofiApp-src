using CofiApp.Contracts.Products;

namespace CofiApp.Contracts.Baskets
{
    public class BasketItemResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public required ProductForBasketItemResponse Product { get; set; }
        public List<BasketItemOptionGroupResponse> BasketItemOptionGroups { get; set; } = [];
    }
}
