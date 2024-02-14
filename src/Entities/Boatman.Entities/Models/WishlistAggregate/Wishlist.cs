namespace Boatman.Entities.Models.WishlistAggregate;

public class Wishlist : BaseEntity<int>, IAggregateRoot
{
    private readonly List<WishlistItem> _items = [];

    public Wishlist(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; private set; }
    public IEnumerable<WishlistItem> Items => _items.AsReadOnly();

    public void AddItem(string apartmentId)
    {
        if (Items.All(i => i.ApartmentId != apartmentId)) _items.Add(new WishlistItem(apartmentId));
    }
}