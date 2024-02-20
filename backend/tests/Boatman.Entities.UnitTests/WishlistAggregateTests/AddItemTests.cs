using Boatman.Entities.Models.WishlistAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.Entities.UnitTests.WishlistAggregate.WishlistTests;

public class AddItemTests
{
    [Fact]
    public void AddItem_ShouldCreateItem_WhenThereIsNoOneWithTheSameApartmentId()
    {
        // Arrange
        const int apartmentId = 1;
        var wishlist = new Wishlist(It.IsAny<string>());

        // Act
        wishlist.AddItem(apartmentId);

        // Assert
        wishlist.Items.Should().Contain(i => i.ApartmentId == apartmentId);
    }

    [Fact]
    public void AddItem_ShouldNotCreateItem_WhenThereIsOneWithTheSameApartmentId()
    {
        // Arrange
        const int apartmentId = 1;
        var wishlist = new Wishlist(It.IsAny<string>());

        // Act
        wishlist.AddItem(apartmentId);
        wishlist.AddItem(apartmentId);

        // Assert
        wishlist.Items.Should().HaveCount(1);
    }
}