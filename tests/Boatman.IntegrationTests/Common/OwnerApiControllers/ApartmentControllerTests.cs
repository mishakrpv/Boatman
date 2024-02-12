using System.Runtime.Versioning;
using Ardalis.GuardClauses;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.OwnerApi.UseCases.Dtos;
using FluentAssertions;

namespace Boatman.IntegrationTests.Common.OwnerApiControllers;

public class ApartmentControllerTests
    : IClassFixture<CommonApiTestFixture>
{
    private readonly HttpClient _client;

    public ApartmentControllerTests(CommonApiTestFixture factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddApartment_ThenGetApartment_ShouldReturnTheSameOne()
    {
        // Arrange
        const string ownerId = "123";
        const decimal rent = 100.00M;
        var apartmentDto = new AddApartmentDto()
        {
            OwnerId = ownerId,
            Rent = rent,
        };
        
        // Act
        var addResponse = await _client.PostAsync("Apartment/Add", JsonContent.Create(apartmentDto));
        addResponse.EnsureSuccessStatusCode();
        var addResult = await addResponse.Content.ReadFromJsonAsync<AddResult>();
        Guard.Against.Null(addResult);
        var getResponse = await _client.GetAsync($"Apartment/Get/{addResult.Id}");
        getResponse.EnsureSuccessStatusCode();
        var apartment = await getResponse.Content.ReadFromJsonAsync<Apartment>();
        
        // Assert
        Guard.Against.Null(apartment);
        apartment.Rent.Should().Be(rent);
        apartment.DownPaymentInMonths.Should().Be(1);
        apartment.OwnerId.Should().Be(ownerId);
    }

    private class AddResult
    {
        public int Id { get; set; }
    }
}