namespace Boatman.Entities.Models.ApartmentAggregate;

public class Photo : BaseEntity<int>
{
    public Photo(string url)
    {
        Url = url;
    }

    public string @Url { get; private set; }
}