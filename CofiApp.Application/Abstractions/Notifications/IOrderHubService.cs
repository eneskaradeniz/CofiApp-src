namespace CofiApp.Application.Abstractions.Notifications
{
    public interface IOrderHubService
    {
        Task SendOrderCreatedEventAsync(string message, CancellationToken cancellationToken = default);
    }
}
