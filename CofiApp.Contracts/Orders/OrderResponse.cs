using CofiApp.Domain.Enums;

namespace CofiApp.Contracts.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; } = [];
    }
}
