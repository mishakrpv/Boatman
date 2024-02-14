namespace Boatman.Entities.Models.ApartmentAggregate;

public abstract class BaseApartment : BaseEntity<int>
{
    public BaseApartment(decimal rent, int downPaymentInMonths)
    {
        Rent = rent;
        DownPaymentInMonths = downPaymentInMonths;
    }

    public decimal Rent { get; protected set; }
    public int DownPaymentInMonths { get; protected set; }

    public double Latitude { get; protected set; }
    public double Longitude { get; protected set; }

    public void SetCoordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}