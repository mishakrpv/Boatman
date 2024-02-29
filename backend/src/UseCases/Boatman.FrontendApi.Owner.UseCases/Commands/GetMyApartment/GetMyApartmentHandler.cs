using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartment;

public class GetMyApartmentHandler : IRequestHandler<GetMyApartment, Response<Apartment>>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly ICache _cache;

    public GetMyApartmentHandler(IRepository<Apartment> apartmentRepo,
        ICache cache)
    {
        _apartmentRepo = apartmentRepo;
        _cache = cache;
    }

    public async Task<Response<Apartment>> Handle(GetMyApartment request, CancellationToken cancellationToken)
    {
        var apartment = await _cache.GetAsync<Apartment>(CacheHelpers.GenerateApartmentCacheKey(request.ApartmentId));

        if (apartment == null)
        {
            var spec = new ApartmentWithPhotosSpecification(request.ApartmentId);
            apartment = await _apartmentRepo.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (apartment == null)
                return new Response<Apartment>
                {
                    StatusCode = 404,
                    Message = "Apartment not found."
                };
        }
        
        return new Response<Apartment>(apartment);
    }
}