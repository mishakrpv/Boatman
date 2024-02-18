namespace Boatman.Entities.Models.ChatAggregate;

public class Chat : BaseEntity<int>, IAggregateRoot
{
    private readonly List<User> _users = [];

    public IEnumerable<User> Users => _users.AsReadOnly();

    public void AddUser(string userId)
    {
        _users.Add(new User(userId));
    }

    public void SendMessage(string userId, string content)
    {
        var user = Users.FirstOrDefault(u => u.ExternalId == userId);

        if (user != null)
        {
            user.SendMessage(content);
        }
    }
}