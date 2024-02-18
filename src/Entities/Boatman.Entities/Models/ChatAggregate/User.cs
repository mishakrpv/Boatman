namespace Boatman.Entities.Models.ChatAggregate;

public class User : BaseEntity<int>
{
    private readonly List<Message> _messages = [];
    
    public User(string externalId)
    {
        ExternalId = externalId;
    }
    
    public string ExternalId { get; private set; }

    public IEnumerable<Message> Messages => _messages.AsReadOnly();

    public void SendMessage(string content)
    {
        _messages.Add(new Message(content));
    }
}