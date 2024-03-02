namespace Boatman.Entities.Models.ChatAggregate;

public class AttachedFile : BaseEntity<int>
{
    public AttachedFile(string messageId, string @uri)
    {
        MessageId = messageId;
        Uri = uri;
    }

    public string MessageId { get; private set; }

    public string @Uri { get; private set; }
}