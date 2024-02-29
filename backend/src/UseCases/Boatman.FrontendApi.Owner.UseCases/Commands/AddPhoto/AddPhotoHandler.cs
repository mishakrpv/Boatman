using Boatman.BlobStorage.Interfaces;
using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.AddPhoto;

public class AddPhotoHandler : IRequestHandler<AddPhoto, Response<string>>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly IBlobStorage _blobStorage;
    private readonly ICache _cache;

    public AddPhotoHandler(IRepository<Apartment> apartmentRepo,
        IBlobStorage blobStorage,
        ICache cache)
    {
        _apartmentRepo = apartmentRepo;
        _blobStorage = blobStorage;
        _cache = cache;
    }

    public async Task<Response<string>> Handle(AddPhoto request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response<string>
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };

        var response = await _blobStorage.UploadAsync(request.Photo);

        if (response.StatusCode != 200)
            return response;
        
        apartment.AddPhoto(response.Value!);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        await _cache.SetAsync<Apartment>(CacheHelpers.GenerateApartmentCacheKey(apartment.Id), apartment);

        return response;
    }
}