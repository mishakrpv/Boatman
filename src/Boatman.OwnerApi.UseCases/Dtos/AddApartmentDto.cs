namespace Boatman.OwnerApi.UseCases.Dtos;

public class AddApartmentDto
{
    public string OwnerId { get; set; } = default!;
    public decimal Rent { get; set; }
    public int DownPaymentInMonths { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}