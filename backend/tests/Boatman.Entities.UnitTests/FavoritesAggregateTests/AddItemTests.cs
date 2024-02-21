using Boatman.Entities.Models.FavoritesAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.Entities.UnitTests.FavoritesAggregateTests;

public class AddItemTests
{
    [Fact]
    public void AddItem_ShouldCreateItem_WhenThereIsNoOneWithTheSameApartmentId()
    {
        // Arrange
        const int apartmentId = 1;
        var favorites = new Favorites(It.IsAny<string>());

        // Act
        favorites.AddItem(apartmentId);

        // Assert
        favorites.Items.Should().Contain(i => i.ApartmentId == apartmentId);
    }

    [Fact]
    public void AddItem_ShouldNotCreateItem_WhenThereIsOneWithTheSameApartmentId()
    {
        // Arrange
        const int apartmentId = 1;
        var favorites = new Favorites(It.IsAny<string>());

        // Act
        favorites.AddItem(apartmentId);
        favorites.AddItem(apartmentId);

        // Assert
        favorites.Items.Should().HaveCount(1);
    }
}