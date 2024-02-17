namespace Boatman.CustomerApi.UseCases.Dtos;

public class SendRequestDto
{
    public int ApartmentId { get; set; }
    public string CustomerId { get; set; } = default!;
}