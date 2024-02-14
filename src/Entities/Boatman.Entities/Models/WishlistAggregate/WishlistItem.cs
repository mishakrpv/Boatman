namespace Boatman.Entities.Models.WishlistAggregate;

public class WishlistItem : BaseEntity<int>
{
    public WishlistItem(string apartmentId)
    {
        ApartmentId = apartmentId;
    }

    public string ApartmentId { get; private set; }
}