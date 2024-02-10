namespace Boatman.Entities.Models.WishlistAggregate;

public class Wishlist : BaseEntity<int>, IAggregateRoot
{
    public string CustomerId { get; private set; }

    public Wishlist(string customerId)
    {
        CustomerId = customerId;
    }

    private readonly List<WishlistItem> _items = [];
    public IEnumerable<WishlistItem> Items => _items.AsReadOnly();

    public void AddItem(string apartmentId)
    {
        if (Items.All(i => i.ApartmentId != apartmentId))
        {
            _items.Add(new WishlistItem(apartmentId));
        }
    }
}