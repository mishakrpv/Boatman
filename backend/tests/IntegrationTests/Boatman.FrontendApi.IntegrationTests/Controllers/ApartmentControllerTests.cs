using System.Net.Http.Json;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.UnitTests.Builders;
using Boatman.FrontendApi.UseCases.Dtos;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Boatman.FrontendApi.IntegrationTests.Controllers;

public class ApartmentControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private HttpClient Client { get; }
    private readonly TestWebApplicationFactory _factory;

    public ApartmentControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
        Client = factory.CreateClient();
    }

    private class AddApartmentResponse
    {
        public int Id { get; set; }
    }

    [Fact]
    public async Task Add_AddsApartment()
    {
        // Arrange
        var apartment = new ApartmentBuilder().Build();
        var content = JsonContent.Create(apartment);
        
        // Act
        var postResponse = await Client.PostAsync("apartment/add", content);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var response = await postResponse.Content.ReadFromJsonAsync<AddApartmentResponse>();
        response.Should().NotBeNull();

        using var scope = _factory.Services.CreateScope();
        var apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
        
        var newApartment = await apartmentRepo.GetByIdAsync(response!.Id);
        newApartment.Should().NotBeNull();
        newApartment!.OwnerId.Should().Be(apartment.OwnerId);
    }

    [Fact]
    public async Task Update_UpdatesApartment()
    {
        // Arrange
        var apartment = new ApartmentBuilder().Build();

        using (var scope = _factory.Services.CreateScope())
        {
            var apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
            
            apartment = await apartmentRepo.AddAsync(apartment);
        }

        var updatedRent = 100.00M;
        var updatedDescription = "This is awesome";
        var updatedDownPaymentInMonths = 2;
        var updatedLatitude = 50.00;
        var updatedLongitude = 20.00;
        var updateApartmentDto = new UpdateApartmentDto
        {
            ApartmentId = apartment.Id,
            Rent = updatedRent,
            Description = updatedDescription,
            DownPaymentInMonths = updatedDownPaymentInMonths,
            Latitude = updatedLatitude,
            Longitude = updatedLongitude
        };
        var content = JsonContent.Create(updateApartmentDto);
        
        // Act
        var putResponse = await Client.PutAsync("apartment/update", content);
        
        // Assert
        putResponse.EnsureSuccessStatusCode();

        using (var scope = _factory.Services.CreateScope())
        {
            var apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
            
            var updatedApartment = await apartmentRepo.GetByIdAsync(apartment.Id);
            
            updatedApartment.Should().NotBeNull();
            updatedApartment!.Rent.Should().Be(updateApartmentDto.Rent);
            updatedApartment.Description.Should().Be(updateApartmentDto.Description);
            updatedApartment.DownPaymentInMonths.Should().Be(updateApartmentDto.DownPaymentInMonths);
            updatedApartment.Latitude.Should().Be(updateApartmentDto.Latitude);
            updatedApartment.Longitude.Should().Be(updateApartmentDto.Longitude);
        }
    }
}