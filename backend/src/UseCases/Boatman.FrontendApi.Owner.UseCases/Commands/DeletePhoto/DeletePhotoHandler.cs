using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.DeletePhoto;

public class DeletePhotoHandler : IRequestHandler<DeletePhoto, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly ICache _cache;

    public DeletePhotoHandler(IRepository<Apartment> apartmentRepo,
        ICache cache)
    {
        _apartmentRepo = apartmentRepo;
        _cache = cache;
    }

    public async Task<Response> Handle(DeletePhoto request, CancellationToken cancellationToken)
    {
        var spec = new ApartmentWithPhotosSpecification(request.ApartmentId);
        var apartment = await _apartmentRepo.FirstOrDefaultAsync(spec);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.DeletePhoto(request.PhotoId);

        await _apartmentRepo.UpdateAsync(apartment);

        await _cache.SetAsync<Apartment>(CacheHelpers.GenerateApartmentCacheKey(apartment.Id), apartment);

        return new Response
        {
            Message = "Photo has been deleted."
        };
    }
}