﻿using System.ComponentModel.DataAnnotations;

namespace Boatman.OwnerApi.UseCases.Dtos;

public class AddApartmentDto
{
    public string OwnerId { get; set; } = default!;
    public decimal Rent { get; set; }
    [Range(1, int.MaxValue)]
    public int DownPaymentInMonths { get; set; } = 1;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}