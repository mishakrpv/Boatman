using System.Net;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Entities.UnitTests.Builders;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Boatman.FrontendApi.IntegrationTests.Controllers;

public class FavoritesControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly IRepository<Favorites> _favRepo;
    
    private HttpClient Client { get; }

    public FavoritesControllerTests(TestWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        _apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
        _favRepo = scope.ServiceProvider.GetRequiredService<IRepository<Favorites>>();
    }

    [Fact]
    public async Task Add_AddsExistingApartment()
    {
        // Arrange
        var apartment = new ApartmentBuilder().Build();
        apartment = await _apartmentRepo.AddAsync(apartment);

        var customerId = "testId";
        
        // Act
        var postResponse = await Client.PostAsync(
            $"favorites/add?apartmentId={apartment.Id}&customerId={customerId}", null);
        
        // Assert
        postResponse.EnsureSuccessStatusCode();
        var spec = new CustomersFavoritesSpecification(customerId);
        var favorites = await _favRepo.FirstOrDefaultAsync(spec);
        favorites.Should().NotBeNull();
        favorites!.Items.Should().Contain(fi => fi.ApartmentId == apartment.Id);
    }

    [Fact]
    public async Task Add_ReturnsNotFound_WhenApartmentDoesNotExist()
    {
        // Act
        var postResponse = await Client.PostAsync(
            "favorites/add?apartmentId=0&customerId=notExisting", null);
        
        // Assert
        postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // [Fact]
    // public async Task Remove_RemovesItem_WhenCustomerHasFavorites()
    // {
    //     // Arrange
    //     var apartmentId = 123;
    //     var favorites = new FavoritesBuilder().WithOneItem(apartmentId);
    //     
    //     await _favRepo.AddAsync(favorites);
    //     
    //     // Act
    //     var postResponse = await Client.PostAsync(
    //         $"favorites/remove?apartmentId={apartmentId}&customerId={favorites.CustomerId}", null);
    //     
    //     // Assert
    //     postResponse.EnsureSuccessStatusCode();
    //     var spec = new CustomersFavoritesSpecification(favorites.CustomerId);
    //     var newFavorites = await _favRepo.FirstOrDefaultAsync(spec);
    //     newFavorites.Should().NotBeNull();
    //     newFavorites!.Items.Should().BeEmpty();
    // }
    
    [Fact]
    public async Task Remove_ReturnsNotFound_WhenCustomerHasNoFavorites()
    {
        // Act
        var postResponse = await Client.PostAsync(
            "favorites/remove?apartmentId=0&customerId=notExisting", null);
        
        // Assert
        postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}