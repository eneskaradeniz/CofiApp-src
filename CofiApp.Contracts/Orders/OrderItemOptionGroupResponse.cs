namespace CofiApp.Contracts.Orders
{
    public class OrderItemOptionGroupResponse
    {
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ProductOptionGroupId { get; set; }
        public string ProductOptionGroupName { get; set; }

        public List<OrderItemOptionResponse> OrderItemOptions { get; set; } = [];
    }
}
