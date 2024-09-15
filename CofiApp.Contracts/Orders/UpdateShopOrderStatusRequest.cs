using CofiApp.Domain.Enums;

namespace CofiApp.Contracts.Orders
{
    public class UpdateShopOrderStatusRequest
    {
        public OrderStatus Status { get; set; }
    }
}
