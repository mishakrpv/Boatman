﻿using System.Runtime.Versioning;
using Ardalis.GuardClauses;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.OwnerApi.UseCases.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Identity.Client;
using Moq;

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

    [Fact]
    public async Task PlanViewing_ShouldCreateViewing()
    {
        // Arrange
        const string ownerId = "123";
        const decimal rent = 100.00M;
        var addApartmentDto = new AddApartmentDto()
        {
            OwnerId = ownerId,
            Rent = rent
        };
        var addResponse = await _client.PostAsync("Apartment/Add", JsonContent.Create(addApartmentDto));
        addResponse.EnsureSuccessStatusCode();
        var addResult = await addResponse.Content.ReadFromJsonAsync<AddResult>();
        Guard.Against.Null(addResult);
        
        // Act
        var planViewingDto1 = new PlanViewingDto()
        {
            ApartmentId = addResult.Id,
            CustomerId = "123",
            StartTime = new DateTime(2000, 5, 1, 0, 0, 0),
            EndTime = new DateTime(2000, 5, 1, 2, 0, 0)
        };
        var planViewingDto2 = new PlanViewingDto()
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
        var scheduleBefore = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Viewing>>();
        scheduleBefore.Should().HaveCount(2);
    }
}