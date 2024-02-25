﻿using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.DeleteApartment;

public class DeleteApartmentHandler : IRequestHandler<DeleteApartment, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public DeleteApartmentHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Response> Handle(DeleteApartment request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response()
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        await _apartmentRepo.DeleteAsync(apartment, cancellationToken);

        return new Response
        {
            Message = $"Apartment with id {apartment.Id} has been deleted."
        };
    }
}