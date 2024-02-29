namespace Boatman.Entities.Models.FavoritesAggregate;

public class FavoriteItem : BaseEntity<int>
{
    public FavoriteItem(int favoritesId, int apartmentId)
    {
        FavoritesId = favoritesId;
        ApartmentId = apartmentId;
    }

    public int FavoritesId { get; private set; }
    
    public int ApartmentId { get; private set; }
}