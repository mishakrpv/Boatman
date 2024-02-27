﻿using System.ComponentModel.DataAnnotations;

namespace Boatman.Entities.Models.ProfilePhotoAggregate;

public class ProfilePhoto : IAggregateRoot
{
    public ProfilePhoto(string userId, string @uri)
    {
        UserId = userId;
        Uri = uri;
    }

    public string UserId { get; private set; }
    
    [Key]
    public string @Uri { get; private set; }

    public void UpdateUri(string @uri)
    {
        Uri = uri;
    }
}