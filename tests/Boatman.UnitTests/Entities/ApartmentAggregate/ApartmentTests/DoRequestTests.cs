using Boatman.Entities.Models.ApartmentAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.Entities.ApartmentAggregate.ApartmentTests;

public class DoRequestTests
{
    [Fact]
    public void DoRequest_ShouldCreateRequest_WhenThereIsNoRequestFromTheSameCustomer()
    {
        // Arrange
        const string customerId = "123";
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>());

        // Act
        apartment.DoRequest(customerId);

        // Assert
        apartment.Requests.Should().Contain(r => r.CustomerId == customerId);
    }

    [Fact]
    public void DoRequest_ShouldNotCreateRequest_WhenThereIsRequestFromTheSameCustomer()
    {
        // Arrange
        const string customerId = "123";
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>());
        apartment.DoRequest(customerId);

        // Act
        apartment.DoRequest(customerId);

        // Assert
        apartment.Requests.Should().HaveCount(1);
    }
}