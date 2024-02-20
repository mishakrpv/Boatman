using Boatman.Entities.Models.ApartmentAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.Entities.UnitTests.ApartmentAggregateTests;

public class SubmitAnApplicationTests
{
    [Fact]
    public void SubmitAnApplication_ShouldCreateRequest_WhenThereIsNoRequestFromTheSameCustomer()
    {
        // Arrange
        const string customerId = "123";
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());

        // Act
        apartment.SubmitAnApplication(customerId);

        // Assert
        apartment.Requests.Should().Contain(r => r.CustomerId == customerId);
    }

    [Fact]
    public void SubmitAnApplication_ShouldNotCreateRequest_WhenThereIsRequestFromTheSameCustomer()
    {
        // Arrange
        const string customerId = "123";
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());

        // Act
        apartment.SubmitAnApplication(customerId);
        apartment.SubmitAnApplication(customerId);

        // Assert
        apartment.Requests.Should().HaveCount(1);
    }
}