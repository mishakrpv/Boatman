namespace Boatman.IntegrationTests.Common;

public class BasicTests
    : IClassFixture<CommonApiTestFixture>
{
    private readonly HttpClient _client;

    public BasicTests(CommonApiTestFixture factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/_health")]
    public async Task Get_ShouldReturnSuccessStatusCode(string url)
    {
        // Arrange

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}