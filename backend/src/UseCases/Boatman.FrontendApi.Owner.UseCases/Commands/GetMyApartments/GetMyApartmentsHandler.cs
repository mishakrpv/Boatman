using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartments;

public class GetMyApartmentsHandler : IRequestHandler<GetMyApartments, Response<IEnumerable<Apartment>>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public GetMyApartmentsHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response<IEnumerable<Apartment>>> Handle(GetMyApartments request, CancellationToken cancellationToken)
    {
        var spec = new OwnersApartmentSpecification(request.OwnerId);
        var apartments = await _apartmentRepo.ListAsync(spec, cancellationToken);

        return new Response<IEnumerable<Apartment>>(apartments.OrderBy(a => a.PublicationDate));
    }
}