using System.ComponentModel.DataAnnotations;

namespace Boatman.OwnerApi.UseCases.Dtos;

public class AddApartmentDto
{
    [Required]
    public string OwnerId { get; set; } = default!;
    [Required]
    public decimal Rent { get; set; }
    [StringLength(300)]
    public string Description { get; set; } = string.Empty;
    [Range(1, 36)]
    public int DownPaymentInMonths { get; set; } = 1;
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
}