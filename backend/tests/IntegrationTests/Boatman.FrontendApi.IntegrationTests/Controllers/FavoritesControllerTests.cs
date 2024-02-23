using Boatman.FrontendApi.IntegrationTests.Fixtures;
using Xunit;

namespace Boatman.FrontendApi.IntegrationTests.Controllers;

public class FavoritesControllerTests
{
    public FavoritesControllerTests()
    {
        var application = new BoatmanWebApplicationFactory();
        Client = application.CreateClient();
    }

    private HttpClient Client { get; }
    
    [Fact]
    public async Task Add_AddsExistingApartment()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
}