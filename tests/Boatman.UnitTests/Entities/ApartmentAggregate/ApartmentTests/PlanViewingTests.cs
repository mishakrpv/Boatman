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
        var startOne = new ViewingDateTime(2000, 12, 5, 0, 0);
        var endOne = new ViewingDateTime(2000, 12, 5, 2, 0);
        var startTwo = new ViewingDateTime(2000, 12, 5, 3, 0);
        var endTwo = new ViewingDateTime(2000, 12, 5, 4, 0);
        apartment.TryPlanViewing(It.IsAny<string>(), startOne, endOne);

        // Act
        bool isSuccess = apartment.TryPlanViewing(It.IsAny<string>(), startTwo, endTwo);

        // Assert
        apartment.Schedule.Should().HaveCount(2);
        isSuccess.Should().Be(true);
    }
    
    [Fact]
    public void PlanViewing_ShouldNotCreateViewing_WhenItConflictWithThePastOne()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>());
        var startOne = new ViewingDateTime(2000, 12, 5, 0, 0);
        var endOne = new ViewingDateTime(2000, 12, 5, 2, 0);
        var startTwo = new ViewingDateTime(2000, 12, 5, 1, 0);
        var endTwo = new ViewingDateTime(2000, 12, 5, 4, 0);
        apartment.TryPlanViewing(It.IsAny<string>(), startOne, endOne);

        // Act
        bool isSuccess = apartment.TryPlanViewing(It.IsAny<string>(), startTwo, endTwo);

        // Assert
        apartment.Schedule.Should().HaveCount(1);
        isSuccess.Should().Be(false);
    }
}