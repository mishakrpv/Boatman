﻿using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.UpdateApartment;

public class UpdateApartmentHandler : IRequestHandler<UpdateApartment, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public UpdateApartmentHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Response> Handle(UpdateApartment request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var apartment = await _apartmentRepo.GetByIdAsync(dto.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.Update(dto.Rent, dto.Description, dto.DownPaymentInMonths);
        apartment.SetCoordinates(dto.Latitude, dto.Longitude);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return new Response
        {
            Message = "Apartment was updated."
        };
    }
}