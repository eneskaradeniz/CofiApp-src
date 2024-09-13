namespace CofiApp.Contracts.Baskets
{
    public class BasketResponse
    {
        public Guid Id { get; set; }
        public List<BasketItemResponse> BasketItems { get; set; } = [];
    }
}
