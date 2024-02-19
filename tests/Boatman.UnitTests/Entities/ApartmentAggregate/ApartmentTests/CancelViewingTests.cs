using Boatman.Entities.Models.ApartmentAggregate;
using FluentAssertions;
using Moq;

namespace Boatman.UnitTests.Entities.ApartmentAggregate.ApartmentTests;

public class CancelViewingTests
{
    [Fact]
    public void CancelViewing_ShouldRemoveViewingFromSchedule()
    {
        // Arrange
        var apartment = new Apartment(It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<int>());

        var startTime = new DateTime(100);
        var endTime = new DateTime(1000);
        bool scheduleSuccess = apartment.TryScheduleViewing(It.IsAny<string>(), startTime, endTime);
        int viewingId = apartment.Schedule.Last().Id;
        // Act
        apartment.CancelViewing(viewingId);

        // Assert
        apartment.Schedule.Should().HaveCount(0);
        scheduleSuccess.Should().Be(true);
    }
}