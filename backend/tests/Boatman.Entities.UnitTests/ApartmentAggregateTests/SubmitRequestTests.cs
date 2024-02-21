using Boatman.Entities.Models.ApartmentAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.Entities.UnitTests.ApartmentAggregateTests;

public class SubmitRequestTests
{
    [Fact]
    public void SubmitRequest_ShouldCreateRequest_WhenThereIsNoRequestFromTheSameCustomer()
    {
        // Arrange
        const string customerId = "123";
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());

        // Act
        apartment.SubmitRequest(customerId);

        // Assert
        apartment.Requests.Should().Contain(r => r.CustomerId == customerId);
    }

    [Fact]
    public void SubmitRequest_ShouldNotCreateRequest_WhenThereIsRequestFromTheSameCustomer()
    {
        // Arrange
        const string customerId = "123";
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());

        // Act
        apartment.SubmitRequest(customerId);
        apartment.SubmitRequest(customerId);

        // Assert
        apartment.Requests.Should().HaveCount(1);
    }
}