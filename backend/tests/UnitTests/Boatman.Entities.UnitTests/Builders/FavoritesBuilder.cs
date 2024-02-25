using Boatman.Entities.Models.FavoritesAggregate;

namespace Boatman.Entities.UnitTests.Builders;

public class FavoritesBuilder
{
    private Favorites _favorites;

    public string CustomerId { get; set; } = "defaultId";

    public FavoritesBuilder()
    {
        _favorites = WithNoItems();
    }
    
    public FavoritesBuilder(string customerId)
    {
        _favorites = new Favorites(customerId);
    }
    
    public Favorites WithNoItems()
    {
        return new Favorites(CustomerId);
    }

    public Favorites WithOneItem(int apartmentId)
    {
        _favorites.AddItem(apartmentId);
            
        return _favorites;
    }
    
    public Favorites Build()
    {
        return _favorites;
    }
}