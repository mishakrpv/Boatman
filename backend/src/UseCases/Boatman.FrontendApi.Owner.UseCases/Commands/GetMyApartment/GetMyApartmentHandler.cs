﻿using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartment;

public class GetMyApartmentHandler : IRequestHandler<GetMyApartment, Response<Apartment>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public GetMyApartmentHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response<Apartment>> Handle(GetMyApartment request, CancellationToken cancellationToken)
    {
        var spec = new ApartmentWithPhotosSpecification(request.ApartmentId);
        var apartment = await _apartmentRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (apartment == null)
            return new Response<Apartment>
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };

        return new Response<Apartment>(apartment);
    }
}