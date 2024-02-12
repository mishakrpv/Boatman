using Ardalis.GuardClauses;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.GetApartment;

public class GetApartmentRequestHandler : IRequestHandler<GetApartmentRequest, Apartment>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public GetApartmentRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Apartment> Handle(GetApartmentRequest request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);
        Guard.Against.Null(apartment);

        return apartment;
    }
}