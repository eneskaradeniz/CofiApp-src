using CofiApp.Application.Abstractions.Notifications;
using CofiApp.Infrastructure.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CofiApp.Infrastructure.Notifications.HubService
{
    internal sealed class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public async Task SendOrderCreatedEventAsync(string message, CancellationToken cancellationToken = default)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderCreatedMessage, message, cancellationToken);
        }
    }
}
