namespace Boatman.Entities.Models.ApartmentAggregate;

public abstract class BaseApartment : BaseEntity<int>
{
    public BaseApartment(decimal rent, string description, int downPaymentInMonths)
    {
        Rent = rent;
        Description = description;
        DownPaymentInMonths = downPaymentInMonths;
    }

    public decimal Rent { get; protected set; }
    public string Description { get; protected set; }
    public int DownPaymentInMonths { get; protected set; }

    public double Latitude { get; protected set; }
    public double Longitude { get; protected set; }
    
    public void Update(decimal rent, string description, int downPaymentInMonths = 1)
    {
        Rent = rent;
        Description = description;
        DownPaymentInMonths = downPaymentInMonths;
    }

    public void SetCoordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}