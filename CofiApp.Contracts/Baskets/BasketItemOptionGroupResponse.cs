namespace CofiApp.Contracts.Baskets
{
    public class BasketItemOptionGroupResponse 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }
        public List<BasketItemOptionResponse> BasketItemOptions { get; set; } = [];
    }
}
