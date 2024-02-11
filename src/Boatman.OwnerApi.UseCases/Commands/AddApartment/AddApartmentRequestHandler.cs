using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.AddApartment;

public class AddApartmentRequestHandler : IRequestHandler<AddApartmentRequest, int>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public AddApartmentRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<int> Handle(AddApartmentRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Apartment(dto.OwnerId, dto.Rent, dto.DownPaymentInMonths);
        entity.SetCoordinates(dto.Latitude, dto.Longitude);
        var apartment = await _apartmentRepo.AddAsync(entity, cancellationToken);

        return apartment.Id;
    }
}