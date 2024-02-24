using System.Net;
using System.Text.Json;
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
    private HttpClient Client { get; }
    private readonly TestWebApplicationFactory _factory;

    public FavoritesControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
        Client = factory.CreateClient();
    }

    [Fact]
    public async Task Add_AddsExistingApartment()
    {
        // Arrange
        const string customerId = "testId";
        var apartment = new ApartmentBuilder().Build();

        using var scope = _factory.Services.CreateScope();
        var apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
        var fevRepo = scope.ServiceProvider.GetRequiredService<IRepository<Favorites>>();
            
        await apartmentRepo.AddAsync(apartment);
            
        // Act
        var postResponse = await Client.PostAsync(
            $"favorites/add?apartmentId={apartment.Id}&customerId={customerId}", null);
            
        // Assert
        postResponse.EnsureSuccessStatusCode();
        var spec = new CustomersFavoritesSpecification(customerId);
        var favorites = await fevRepo.FirstOrDefaultAsync(spec);
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

    [Fact]
    public async Task Remove_RemovesItem_WhenCustomerHasFavorites()
    {
        // Arrange
        const int apartmentId = 123;
        var favorites = new FavoritesBuilder().WithOneItem(apartmentId);

        using (var scope = _factory.Services.CreateScope())
        {
            var fevRepo = scope.ServiceProvider.GetRequiredService<IRepository<Favorites>>();
        
            await fevRepo.AddAsync(favorites);
        }
        
        // Act
        var postResponse = await Client.PostAsync(
            $"favorites/remove?apartmentId={apartmentId}&customerId={favorites.CustomerId}", null);
        
        // Assert
        postResponse.EnsureSuccessStatusCode();
        
        using (var scope = _factory.Services.CreateScope())
        {
            var fevRepo = scope.ServiceProvider.GetRequiredService<IRepository<Favorites>>();
            
            var spec = new CustomersFavoritesSpecification(favorites.CustomerId);
            var newFavorites = await fevRepo.FirstOrDefaultAsync(spec);
            newFavorites.Should().NotBeNull();
            newFavorites!.Items.Should().BeEmpty();
        }
    }
    
    [Fact]
    public async Task Remove_ReturnsNotFound_WhenCustomerHasNoFavorites()
    {
        // Act
        var postResponse = await Client.PostAsync(
            "favorites/remove?apartmentId=0&customerId=notExisting", null);
        
        // Assert
        postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task MyFavorites_ReturnsFavorites()
    {
        // Arrange
        const int apartmentId = 123;
        var favorites = new FavoritesBuilder().WithOneItem(apartmentId);

        using (var scope = _factory.Services.CreateScope())
        {
            var fevRepo = scope.ServiceProvider.GetRequiredService<IRepository<Favorites>>();
        
            await fevRepo.AddAsync(favorites);
        }
        
        // Act
        var getResponse = await Client.GetAsync($"favorites/{favorites.CustomerId}");
        
        // Assert
        getResponse.EnsureSuccessStatusCode();
        var favoritesAsString = await getResponse.Content.ReadAsStringAsync();
        favoritesAsString.Should().BeEquivalentTo(JsonSerializer.Serialize(favorites));
    }
}