using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Boatman.FrontendApi.IntegrationTests.Controllers;

public class ApartmentControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    
    private HttpClient Client { get; }

    public ApartmentControllerTests(TestWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        _apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
    }
}