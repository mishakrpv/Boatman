using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.UnitTests.Builders;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Boatman.FrontendApi.IntegrationTests.Controllers;

public class FavoritesControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private HttpClient Client { get; }

    public FavoritesControllerTests(TestWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        _apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
    }

    [Fact]
    public async Task Add_AddsExistingApartment()
    {
        // // Arrange
        // var apartment = new ApartmentBuilder().Build();
        // await _apartmentRepo.AddAsync(apartment);
        //
        // var keyValues = new { apartmentId = 1, customerId = "123" };
        // var content = new StringContent(keyValues.ToString()!);
        //
        // // Act
        // var postResponse = await Client.PostAsync("favorites/add", content);
        //
        // // Assert
        // postResponse.EnsureSuccessStatusCode();
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