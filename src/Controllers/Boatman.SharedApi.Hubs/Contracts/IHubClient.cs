namespace Boatman.CommonApi.Hubs.Contracts;

public interface IHubClient
{
    Task ReceiveMessage(string message);
}