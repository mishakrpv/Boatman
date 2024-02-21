using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.GetApartment;

public class GetApartmentRequestHandler : IRequestHandler<GetApartmentRequest, Response<Apartment>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public GetApartmentRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response<Apartment>> Handle(GetApartmentRequest request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response<Apartment>
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };

        return new Response<Apartment>(apartment);
    }
}