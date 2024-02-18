namespace Boatman.CustomerApi.UseCases.Dtos;

public class ApartmentCustomerDto
{
    public int ApartmentId { get; set; }
    public string CustomerId { get; set; } = default!;
}