namespace Boatman.Entities.Models.ApartmentAggregate;

public struct ViewingDateTime
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }

    public ViewingDateTime(int year, int month, int day, int hour, int minute)
    {
        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
    }
}