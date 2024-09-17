namespace CofiApp.Contracts.Baskets
{
    public class BasketResponse
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BasketItemResponse> BasketItems { get; set; } = [];
    }
}
