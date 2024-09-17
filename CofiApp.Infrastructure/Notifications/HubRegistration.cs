using CofiApp.Infrastructure.Notifications.Hubs;
using Microsoft.AspNetCore.Builder;

namespace CofiApp.Infrastructure.Notifications
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<OrderHub>("/orders-hub");
        }
    }
}
