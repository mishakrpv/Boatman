using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.OwnerApi.UseCases.Commands.AddApartment;
using Boatman.OwnerApi.UseCases.Dtos;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.UseCases.OwnerApi;

public class AddApartmentTests
{
    private readonly Mock<IRepository<Apartment>> _mockApartmentRepo = new();
    
    [Fact]
    public async Task Handle_ShouldReturnSameId_WhenRequestIsFine()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>());
        _mockApartmentRepo.Setup(ar => ar.AddAsync(It.IsAny<Apartment>(), default))
            .ReturnsAsync(apartment);
            
        var request = new AddApartmentRequest(new AddApartmentDto()
        {
            OwnerId = It.IsAny<string>(),
            Rent = It.IsAny<decimal>(),
            DownPaymentInMonths = It.IsAny<int>(),
            Latitude = It.IsAny<double>(),
            Longitude = It.IsAny<double>()
        });

        var handler = new AddApartmentRequestHandler(_mockApartmentRepo.Object);

        // Act
        int id = await handler.Handle(request, default);
        
        // Assert
        id.Should().Be(apartment.Id);
    }
}