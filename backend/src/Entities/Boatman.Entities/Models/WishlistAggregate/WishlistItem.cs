namespace Boatman.Entities.Models.WishlistAggregate;

public class WishlistItem : BaseEntity<int>
{
    public WishlistItem(int apartmentId)
    {
        ApartmentId = apartmentId;
    }

    public int ApartmentId { get; private set; }
}