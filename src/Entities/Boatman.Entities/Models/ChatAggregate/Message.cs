namespace Boatman.Entities.Models.ChatAggregate;

public class Message : BaseEntity<int>
{
    public Message(string content)
    {
        Content = content;
    }
    
    public string Content { get; private set; }

    public DateTime SendDate { get; private set; } = DateTime.Now;
}