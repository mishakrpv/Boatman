using Boatman.Entities.Models.ApartmentAggregate;

namespace Boatman.Entities.UnitTests.Builders;

public class ApartmentBuilder
{
    private Apartment _apartment;
    
    public string OwnerId { get; set; } = "123";
    public decimal Rent { get; set; } = 50.00M;
    public string Description { get; set; } = "Very Good Apartment";

    public ApartmentBuilder()
    {
        _apartment = WithDefaultValues();
    }

    public Apartment WithDefaultValues()
    {
        return new Apartment(OwnerId, Rent, Description);
    }

    public Apartment Build()
    {
        return _apartment;
    }
}