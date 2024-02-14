using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.OwnerApi.UseCases.Commands.MyApartments;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.UseCases.OwnerApi;

public class MyApartmentsTests
{
    private const string OwnerId = "123";
    private readonly Mock<IRepository<Apartment>> _mockApartmentRepo = new();

    [Fact]
    public async Task Handle_ShouldReturnApartments_WhenRequestIsFine()
    {
        // Arrange
        var apartments = GetApartments();
        _mockApartmentRepo.Setup(ar => ar.ListAsync(
            It.IsAny<OwnersApartmentSpecification>(), default)).ReturnsAsync(apartments);

        var request = new MyApartmentsRequest(OwnerId);
        var handler = new MyApartmentsRequestHandler(_mockApartmentRepo.Object);

        // Act
        var response = await handler.Handle(request, default);

        // Assert
        var array = response.Value as Apartment[] ?? response.Value!.ToArray();
        array.Should().HaveCount(3);
        array.Should().AllSatisfy(a => a.OwnerId.Should().Be(OwnerId));
    }

    private List<Apartment> GetApartments()
    {
        return
        [
            new Apartment(OwnerId, It.IsAny<decimal>(), It.IsAny<int>()),
            new Apartment(OwnerId, It.IsAny<decimal>(), It.IsAny<int>()),
            new Apartment(OwnerId, It.IsAny<decimal>(), It.IsAny<int>())
        ];
    }
}