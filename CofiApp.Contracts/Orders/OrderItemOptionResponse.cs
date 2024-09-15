namespace CofiApp.Contracts.Orders
{
    public class OrderItemOptionResponse
    {
        public Guid Id { get; set; }
        public Guid OrderItemOptionGroupId { get; set; }
        public Guid ProductOptionId { get; set; }
        public string ProductOptionName { get; set; }
        public decimal ProductOptionPrice { get; set; }
    }
}
