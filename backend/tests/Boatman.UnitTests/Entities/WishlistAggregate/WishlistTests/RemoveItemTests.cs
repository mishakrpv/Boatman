using Boatman.Entities.Models.WishlistAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.Entities.WishlistAggregate.WishlistTests;

public class RemoveItemTests
{
    [Fact]
    public void RemoveItem_ShouldRemoveItemFromTheWishlist()
    {
        // Arrange
        const int apartmentId = 1;
        var wishlist = new Wishlist(It.IsAny<string>());

        // Act
        wishlist.AddItem(apartmentId);
        wishlist.RemoveItem(apartmentId);

        // Assert
        wishlist.Items.Should().HaveCount(0);
    }
}