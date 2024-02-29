using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartments;

public class GetMyApartmentsHandler : IRequestHandler<GetMyApartments, Response<IEnumerable<Apartment>>>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly ICache _cache;

    public GetMyApartmentsHandler(IRepository<Apartment> apartmentRepo,
        ICache cache)
    {
        _apartmentRepo = apartmentRepo;
        _cache = cache;
    }

    public async Task<Response<IEnumerable<Apartment>>> Handle(GetMyApartments request, CancellationToken cancellationToken)
    {
        var apartments = await _cache.GetAsync<IEnumerable<Apartment>>(
            CacheHelpers.GenerateOwnersApartmentsCacheKey(request.OwnerId));

        if (apartments == null)
        {
            var spec = new OwnersApartmentSpecification(request.OwnerId);
            apartments = await _apartmentRepo.ListAsync(spec, cancellationToken);
        }

        return new Response<IEnumerable<Apartment>>(apartments.OrderBy(a => a.PublicationDate));
    }
}