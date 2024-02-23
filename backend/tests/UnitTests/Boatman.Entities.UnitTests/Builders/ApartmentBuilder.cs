using Boatman.Entities.Models.ApartmentAggregate;

namespace Boatman.Entities.UnitTests.Builders;

public class ApartmentBuilder
{
    private const string OwnerId = "123";
    private const decimal Rent = 50.00M;
    private const string Description = "Very Good Apartment";
    private readonly Apartment _apartment = WithDefaultValues();

    public static Apartment WithDefaultValues()
    {
        return new Apartment(OwnerId, Rent, Description);
    }

    public Apartment Build()
    {
        return _apartment;
    }
}