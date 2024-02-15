using Ardalis.GuardClauses;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.OwnerApi.UseCases.Dtos;
using FluentAssertions;
using Newtonsoft.Json;

namespace Boatman.IntegrationTests.Common.OwnerApiControllers;

// TODO: At the time of the current commit they are completely non-working
public class ApartmentControllerTests
    : IClassFixture<CommonApiTestFixture>
{
    private readonly HttpClient _client;

    public ApartmentControllerTests(CommonApiTestFixture factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddTwoApartments_ThenGetTwoApartments_ShouldReturnTwoApartmentsWithDifferentIds()
    {
        // Arrange
        const string ownerId1 = "123";
        const decimal rent1 = 100.00M;
        const string ownerId2 = "321";
        const decimal rent2 = 50.00M;
        var apartmentDto1 = new AddApartmentDto
        {
            OwnerId = ownerId1,
            Rent = rent1
        };
        var apartmentDto2 = new AddApartmentDto
        {
            OwnerId = ownerId2,
            Rent = rent2
        };

        // Act
        var addResponse1 = await _client.PostAsync("Apartment/Add", JsonContent.Create(apartmentDto1));
        addResponse1.EnsureSuccessStatusCode();
        var addResult1 = await addResponse1.Content.ReadFromJsonAsync<AddResult>();
        Guard.Against.Null(addResult1);
        var getResponse1 = await _client.GetAsync($"Apartment/Get/{addResult1.Id}");
        getResponse1.EnsureSuccessStatusCode();
        var apartment1Str = await getResponse1.Content.ReadAsStringAsync();
        var apartment1 = JsonConvert.DeserializeObject<Apartment>(apartment1Str);

        var addResponse2 = await _client.PostAsync("Apartment/Add", JsonContent.Create(apartmentDto2));
        addResponse2.EnsureSuccessStatusCode();
        var addResult2 = await addResponse2.Content.ReadFromJsonAsync<AddResult>();
        Guard.Against.Null(addResult2);
        var getResponse2 = await _client.GetAsync($"Apartment/Get/{addResult2.Id}");
        getResponse2.EnsureSuccessStatusCode();
        var apartment2 = await getResponse2.Content.ReadFromJsonAsync<Apartment>();

        // Assert
        Guard.Against.Null(apartment1);
        apartment1.Rent.Should().Be(rent1);
        apartment1.DownPaymentInMonths.Should().Be(1);
        apartment1.OwnerId.Should().Be(ownerId1);

        Guard.Against.Null(apartment2);
        apartment2.Rent.Should().Be(rent2);
        apartment2.DownPaymentInMonths.Should().Be(1);
        apartment2.OwnerId.Should().Be(ownerId2);

        apartment1.Id.Should().NotBe(apartment2.Id);
    }

    [Fact]
    public async Task PlanViewing_ShouldCreateViewings_WhenDatesDoesNotConflict()
    {
        // Arrange
        const string ownerId = "123";
        const decimal rent = 100.00M;
        var addApartmentDto = new AddApartmentDto
        {
            OwnerId = ownerId,
            Rent = rent
        };
        var addResponse = await _client.PostAsync("Apartment/Add", JsonContent.Create(addApartmentDto));
        addResponse.EnsureSuccessStatusCode();
        var addResult = await addResponse.Content.ReadFromJsonAsync<AddResult>();
        Guard.Against.Null(addResult);

        // Act
        var planViewingDto1 = new ScheduleViewingDto
        {
            ApartmentId = addResult.Id,
            CustomerId = "123",
            StartTime = new DateTime(2000, 5, 1, 0, 0, 0),
            EndTime = new DateTime(2000, 5, 1, 2, 0, 0)
        };
        var planViewingDto2 = new ScheduleViewingDto
        {
            ApartmentId = addResult.Id,
            CustomerId = "123",
            StartTime = new DateTime(2000, 5, 1, 3, 0, 0),
            EndTime = new DateTime(2000, 5, 1, 4, 0, 0)
        };
        var planResponse1 = await _client.PostAsync("Apartment/Schedule/Plan", JsonContent.Create(planViewingDto1));
        planResponse1.EnsureSuccessStatusCode();
        var planResponse2 = await _client.PostAsync("Apartment/Schedule/Plan", JsonContent.Create(planViewingDto2));
        planResponse2.EnsureSuccessStatusCode();

        // Assert
        var getResponse = await _client.GetAsync($"Apartment/Schedule/{addResult.Id}");
        getResponse.EnsureSuccessStatusCode();
        var schedule = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Viewing>>();
        schedule.Should().HaveCount(2);
    }

    [Fact]
    public async Task PlanViewing_ShouldCreateOneViewing_WhenDatesConflict()
    {
        // Arrange
        const string ownerId = "123";
        const decimal rent = 100.00M;
        var addApartmentDto = new AddApartmentDto
        {
            OwnerId = ownerId,
            Rent = rent
        };
        var addResponse = await _client.PostAsync("Apartment/Add", JsonContent.Create(addApartmentDto));
        addResponse.EnsureSuccessStatusCode();
        var addResult = await addResponse.Content.ReadFromJsonAsync<AddResult>();
        Guard.Against.Null(addResult);

        // Act
        var planViewingDto1 = new ScheduleViewingDto
        {
            ApartmentId = addResult.Id,
            CustomerId = "123",
            StartTime = new DateTime(2000, 5, 1, 0, 0, 0),
            EndTime = new DateTime(2000, 5, 1, 2, 0, 0)
        };
        var planViewingDto2 = new ScheduleViewingDto
        {
            ApartmentId = addResult.Id,
            CustomerId = "123",
            StartTime = new DateTime(2000, 5, 1, 1, 0, 0),
            EndTime = new DateTime(2000, 5, 1, 2, 0, 0)
        };
        var planResponse1 = await _client.PostAsync("Apartment/Schedule/Plan", JsonContent.Create(planViewingDto1));
        planResponse1.EnsureSuccessStatusCode();
        var planResponse2 = await _client.PostAsync("Apartment/Schedule/Plan", JsonContent.Create(planViewingDto2));

        // Assert
        var getResponse = await _client.GetAsync($"Apartment/Schedule/{addResult.Id}");
        getResponse.EnsureSuccessStatusCode();
        var scheduleBefore = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Viewing>>();
        scheduleBefore.Should().HaveCount(1);
    }

    private class AddResult
    {
        public int Id { get; }
    }
}