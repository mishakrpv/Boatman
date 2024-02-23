using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
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