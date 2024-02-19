using Boatman.Entities.Models.ApartmentAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.Entities.ApartmentAggregate.ApartmentTests;

public class ScheduleViewingTests
{
    [Fact]
    public void ScheduleViewing_ShouldCreateViewing_WhenItDoesNotConflictWithThePastOne()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());
        var startOne = new DateTime(2000, 12, 5, 0, 0, 0);
        var endOne = new DateTime(2000, 12, 5, 2, 0, 0);
        var startTwo = new DateTime(2000, 12, 5, 3, 0, 0);
        var endTwo = new DateTime(2000, 12, 5, 4, 0, 0);
        apartment.TryScheduleViewing(It.IsAny<string>(), startOne, endOne);

        // Act
        var isSuccess = apartment.TryScheduleViewing(It.IsAny<string>(), startTwo, endTwo);

        // Assert
        apartment.Schedule.Should().HaveCount(2);
        isSuccess.Should().Be(true);
    }

    [Fact]
    public void ScheduleViewing_ShouldNotCreateViewing_WhenItConflictWithThePastOne()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());
        var startOne = new DateTime(2000, 12, 5, 0, 0, 0);
        var endOne = new DateTime(2000, 12, 5, 2, 0, 0);
        var startTwo = new DateTime(2000, 12, 5, 1, 0, 0);
        var endTwo = new DateTime(2000, 12, 5, 4, 0, 0);
        apartment.TryScheduleViewing(It.IsAny<string>(), startOne, endOne);

        // Act
        var isSuccess = apartment.TryScheduleViewing(It.IsAny<string>(), startTwo, endTwo);

        // Assert
        apartment.Schedule.Should().HaveCount(1);
        isSuccess.Should().Be(false);
    }
}