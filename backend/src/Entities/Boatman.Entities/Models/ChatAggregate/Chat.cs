namespace Boatman.Entities.Models.ChatAggregate;

public class Chat : BaseEntity<int>, IAggregateRoot
{
    private readonly List<Message> _messages = new();

    public IEnumerable<Message> Messages => _messages.AsReadOnly();    
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

    public void SendMessage(string fromUserId, string messageId, string fromUserName, string content)
    {
        var message = new Message(Id, messageId, fromUserId, fromUserName, content);
        
        _messages.Add(message);
    }

    public void AttachFileToMessage(string messageId, string uri)
    {
        var message = Messages.FirstOrDefault(m => m.Id == messageId);

        if (message != null)
        {
            message.AttachFile(uri);
        }
    }
}