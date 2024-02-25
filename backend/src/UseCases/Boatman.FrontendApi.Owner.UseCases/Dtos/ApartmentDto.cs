using System.ComponentModel.DataAnnotations;

namespace Boatman.FrontendApi.Owner.UseCases.Dtos;

public abstract class ApartmentDto
{
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