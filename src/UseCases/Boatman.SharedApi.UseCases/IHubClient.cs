using Boatman.SharedApi.Hubs.Schemes.ReceiveSchemes;

namespace Boatman.SharedApi.Hubs.Contracts;

public interface IHubClient
{
    Task ReceiveMessage(string message);

    Task ReceiveNotification(ReceiveNotificationScheme notificationScheme);
}