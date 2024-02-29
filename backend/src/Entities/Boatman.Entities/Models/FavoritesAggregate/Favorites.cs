namespace Boatman.Entities.Models.FavoritesAggregate;

public class Favorites : BaseEntity<int>, IAggregateRoot
{
    private readonly List<FavoriteItem> _items = [];

    public Favorites(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; private set; }
    
    public IEnumerable<FavoriteItem> Items => _items.AsReadOnly();

    public void AddItem(int apartmentId)
    {
        if (Items.All(i => i.ApartmentId != apartmentId))
        {
            _items.Add(new FavoriteItem(Id, apartmentId));
        }
    }

    public void RemoveItem(int apartmentId)
    {
        var item = Items.FirstOrDefault(i => i.ApartmentId == apartmentId);

        if (item != null)
        {
            _items.Remove(item);
        }
    }
}