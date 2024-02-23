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

        var customerId = "312";
        
        // Act
        var postResponse = await Client.PostAsync(
            $"favorites/add?apartmentId={apartment.Id}&customerId={customerId}", null);
        
        // Assert
        postResponse.EnsureSuccessStatusCode();
        var spec = new CustomersFavoritesSpecification(customerId);
        var favorites = await _favRepo.FirstOrDefaultAsync(spec);
        favorites?.Should().NotBeNull();
        favorites!.Items.Should().Contain(fi => fi.ApartmentId == apartment.Id);
    }

    [Fact]
    public async Task Add_ReturnsNotFound_WhenApartmentDoesNotExist()
    {
        // // Arrange
        // var keyValues = new { apartmentId = 1, customerId = "123" };
        // var content = new StringContent(keyValues.ToString()!);
        //
        // // Act
        // var postResponse = await Client.PostAsync("favorites/add", content);
        //
        // // Assert
        // postResponse.EnsureSuccessStatusCode();
    }
}