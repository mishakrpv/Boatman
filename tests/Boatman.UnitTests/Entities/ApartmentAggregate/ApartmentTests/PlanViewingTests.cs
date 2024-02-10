using Boatman.Entities.Models.ApartmentAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.Entities.ApartmentAggregate.ApartmentTests;

public class PlanViewingTests
{
    [Fact]
    public void PlanViewing_ShouldCreateViewing_WhenItDoesNotConflictWithThePastOne()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>());
        var startOne = new DateTimeOffset(2000, 12, 5, 0, 0, 0, TimeSpan.Zero);
        var endOne = new DateTimeOffset(2000, 12, 5, 2, 0, 0, TimeSpan.Zero);
        var startTwo = new DateTimeOffset(2000, 12, 5, 3, 0, 0, TimeSpan.Zero);
        var endTwo = new DateTimeOffset(2000, 12, 5, 4, 0, 0, TimeSpan.Zero);
        apartment.PlanViewing(It.IsAny<string>(), startOne, endOne);

        // Act
        apartment.PlanViewing(It.IsAny<string>(), startTwo, endTwo);

        // Assert
        apartment.Schedule.Should().HaveCount(2);
    }
    
    [Fact]
    public void PlanViewing_ShouldNotCreateViewing_WhenItConflictWithThePastOne()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>());
        var startOne = new DateTimeOffset(2000, 12, 5, 0, 0, 0, TimeSpan.Zero);
        var endOne = new DateTimeOffset(2000, 12, 5, 2, 0, 0, TimeSpan.Zero);
        var startTwo = new DateTimeOffset(2000, 12, 5, 1, 0, 0, TimeSpan.Zero);
        var endTwo = new DateTimeOffset(2000, 12, 5, 4, 0, 0, TimeSpan.Zero);
        apartment.PlanViewing(It.IsAny<string>(), startOne, endOne);

        // Act
        apartment.PlanViewing(It.IsAny<string>(), startTwo, endTwo);

        // Assert
        apartment.Schedule.Should().HaveCount(1);
    }
}