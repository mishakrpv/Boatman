using System.ComponentModel.DataAnnotations;

namespace Boatman.Entities.Models.ProfilePictureAggregate;

public class ProfilePicture : IAggregateRoot
{
    public ProfilePicture(string userId, string url)
    {
        UserId = userId;
        Url = url;
    }

    public string UserId { get; private set; }
    
    [Key]
    public string @Url { get; private set; }
}