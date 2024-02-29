using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.DeleteApartment;

public class DeleteApartmentHandler : IRequestHandler<DeleteApartment, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly ICache _cache;

    public DeleteApartmentHandler(IRepository<Apartment> apartmentRepo,
        ICache cache)
    {
        _apartmentRepo = apartmentRepo;
        _cache = cache;
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

        await _cache.RemoveAsync(CacheHelpers.GenerateApartmentCacheKey(apartment.Id));

        return new Response
        {
            Message = $"Apartment with id {apartment.Id} has been deleted."
        };
    }
}