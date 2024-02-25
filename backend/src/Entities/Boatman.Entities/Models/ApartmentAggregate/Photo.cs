namespace Boatman.Entities.Models.ApartmentAggregate;

public class Photo : BaseEntity<int>
{
    public Photo(string uri)
    {
        Uri = uri;
    }

    public string @Uri { get; private set; }
}