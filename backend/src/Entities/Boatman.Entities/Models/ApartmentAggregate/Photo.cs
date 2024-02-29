namespace Boatman.Entities.Models.ApartmentAggregate;

public class Photo : BaseEntity<int>
{
    public Photo(int apartmentId, string @uri)
    {
        ApartmentId = apartmentId;
        Uri = uri;
    }

    public int ApartmentId { get; private set; }

    public string @Uri { get; private set; }
}