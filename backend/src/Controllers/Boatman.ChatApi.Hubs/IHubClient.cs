namespace Boatman.ChatApi.Hubs;

public interface IHubClient
{
    Task ReceiveMessageId(string messageId);

    Task ReceiveChatId(int chatId);
    
    Task ReceiveMessage(string fromUser, string message);

    Task ReceiveNotification(string fromUser, string eventType);
}