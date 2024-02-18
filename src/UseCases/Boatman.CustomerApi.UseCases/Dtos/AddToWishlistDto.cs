namespace Boatman.CustomerApi.UseCases.Dtos;

public class AddToWishlistDto
{
    public int ApartmentId { get; set; }
    public string CustomerId { get; set; } = default!;
}