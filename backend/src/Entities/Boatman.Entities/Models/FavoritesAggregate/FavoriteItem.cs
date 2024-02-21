namespace Boatman.Entities.Models.FavoritesAggregate;

public class FavoriteItem : BaseEntity<int>
{
    public FavoriteItem(int apartmentId)
    {
        ApartmentId = apartmentId;
    }

    public int ApartmentId { get; private set; }
}