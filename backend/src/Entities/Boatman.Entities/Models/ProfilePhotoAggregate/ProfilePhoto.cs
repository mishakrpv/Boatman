using System.ComponentModel.DataAnnotations;

namespace Boatman.Entities.Models.ProfilePictureAggregate;

public class ProfilePhoto : IAggregateRoot
{
    public ProfilePhoto(string userId, string url)
    {
        UserId = userId;
        Url = url;
    }

    public string UserId { get; private set; }
    
    [Key]
    public string @Url { get; private set; }
}