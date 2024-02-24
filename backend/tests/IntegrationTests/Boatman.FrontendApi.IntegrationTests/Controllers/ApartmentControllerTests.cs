using System.Net.Http.Json;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.UnitTests.Builders;
using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Models.Response;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Boatman.FrontendApi.IntegrationTests.Controllers;

public class ApartmentControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    
    private HttpClient Client { get; }
    private TestWebApplicationFactory _factory;

    public ApartmentControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
        Client = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        _apartmentRepo = scope.ServiceProvider.GetRequiredService<IRepository<Apartment>>();
    }

    private class AddApartmentResponse
    {
        public int Id { get; set; }
    }
    
    private IRepository<T> NewRepository<T>() where T : class, IAggregateRoot
    {
        var scope = _factory.Services.CreateScope();

        return scope.ServiceProvider.GetRequiredService<IRepository<T>>();
    }

    [Fact]
    public async Task Add_AddsApartment()
    {
        // Arrange
        var apartment = new ApartmentBuilder().Build();
        var content = JsonContent.Create<Apartment>(apartment);
        
        // Act
        var postResponse = await Client.PostAsync("apartment/add", content);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var response = await postResponse.Content.ReadFromJsonAsync<AddApartmentResponse>();
        response.Should().NotBeNull();
        var newApartment = await _apartmentRepo.GetByIdAsync(response!.Id);
        newApartment.Should().NotBeNull();
        newApartment!.OwnerId.Should().Be(apartment.OwnerId);
    }

    [Fact]
    public async Task Update_UpdatesApartment()
    {
        // Arrange
        var apartment = new ApartmentBuilder().Build();
        apartment = await _apartmentRepo.AddAsync(apartment);

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
        var content = JsonContent.Create<UpdateApartmentDto>(updateApartmentDto);
        
        // Act
        var putResponse = await Client.PutAsync("apartment/update", content);
        
        // Assert
        putResponse.EnsureSuccessStatusCode();
        var newApartmentRepo = NewRepository<Apartment>();
        var updatedApartment = await newApartmentRepo.GetByIdAsync(apartment.Id);
        updatedApartment.Should().NotBeNull();
        updatedApartment!.Rent.Should().Be(updateApartmentDto.Rent);
        updatedApartment!.Description.Should().Be(updateApartmentDto.Description);
        updatedApartment!.DownPaymentInMonths.Should().Be(updateApartmentDto.DownPaymentInMonths);
        updatedApartment!.Latitude.Should().Be(updateApartmentDto.Latitude);
        updatedApartment!.Longitude.Should().Be(updateApartmentDto.Longitude);
    }
}