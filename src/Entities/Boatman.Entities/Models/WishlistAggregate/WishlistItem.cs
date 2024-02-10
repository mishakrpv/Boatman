namespace Boatman.Entities.Models.WishlistAggregate;

public class WishlistItem
{
    public string ApartmentId { get; private set; }

    public WishlistItem(string apartmentId)
    {
        ApartmentId = apartmentId;
    }
}