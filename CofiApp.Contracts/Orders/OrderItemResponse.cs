namespace CofiApp.Contracts.Orders
{
    public class OrderItemResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public List<OrderItemOptionGroupResponse> OrderItemOptionGroups { get; set; } = [];
    }
}
