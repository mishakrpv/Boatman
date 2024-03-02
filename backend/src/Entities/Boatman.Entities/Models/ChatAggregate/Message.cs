using System.ComponentModel.DataAnnotations;

namespace Boatman.Entities.Models.ChatAggregate;

public sealed class Message : BaseEntity<string>
{
    private readonly List<AttachedFile> _files = new();
    
    public Message(int chatId, string id, string fromUserId, string fromUserName, string content)
    {
        ChatId = chatId;
        Id = id;
        FromUserId = fromUserId;
        FromUserName = fromUserName;
        Content = content;
    }

    public int ChatId { get; private set; }

    public string FromUserId { get; private set; }
    public string FromUserName { get; private set; }
    public string Content { get; private set; }
    public IEnumerable<AttachedFile> Files => _files.AsReadOnly();
    public DateTime WasSentAt { get; private set; } = DateTime.UtcNow;

    public void AttachFile(string uri)
    {
        _files.Add(new AttachedFile(Id, uri));
    }
}