using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.AddApartment;

public class AddApartmentHandler : IRequestHandler<AddApartment, Response<int>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public AddApartmentHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response<int>> Handle(AddApartment request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Apartment(dto.OwnerId, dto.Rent, dto.Description, dto.DownPaymentInMonths);
        entity.SetCoordinates(dto.Latitude, dto.Longitude);
        var apartment = await _apartmentRepo.AddAsync(entity, cancellationToken);

        return new Response<int>(apartment.Id);
    }
}