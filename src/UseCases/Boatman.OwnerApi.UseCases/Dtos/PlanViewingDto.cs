﻿using System.ComponentModel.DataAnnotations;
using Boatman.Entities.Models.ApartmentAggregate;

namespace Boatman.OwnerApi.UseCases.Dtos;

public class PlanViewingDto
{
    [Required]
    public int ApartmentId { get; set; }
    [Required]
    public string CustomerId { get; set; } = default!;
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}