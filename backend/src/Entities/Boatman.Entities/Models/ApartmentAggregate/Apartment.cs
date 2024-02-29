namespace Boatman.Entities.Models.ApartmentAggregate;

public class Apartment : BaseApartment, IAggregateRoot
{
    private readonly List<Photo> _photos = [];

    public Apartment(string ownerId, decimal rent, string description, int downPaymentInMonths = 1)
        : base(rent, description, downPaymentInMonths)
    {
        OwnerId = ownerId;
    }

    public string OwnerId { get; private set; }

    public IEnumerable<Photo> Photos => _photos.AsReadOnly();
    public bool IsVisible { get; private set; } = true;
    public DateTime PublicationDate { get; private set; } = DateTime.UtcNow;

    public void SetStatus(bool isVisible)
    {
        IsVisible = isVisible;
    }
    
    public void AddPhoto(string uri)
    {
        _photos.Add(new Photo(Id, uri));
    }

    public void DeletePhoto(string uri)
    {
        var photo = Photos.FirstOrDefault(p => p.Uri == uri);

        if (photo != null)
        {
            _photos.Remove(photo);
        }
    }
    
    public void DeletePhoto(int photoId)
    {
        var photo = Photos.FirstOrDefault(p => p.Id == photoId);

        if (photo != null)
        {
            _photos.Remove(photo);
        }
    }
}