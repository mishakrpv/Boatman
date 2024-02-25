using Boatman.BlobStorage.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.AddPhoto;

public class AddPhotoRequestHandler : IRequestHandler<AddPhotoRequest, Response<string>>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly IBlobStorage _blobStorage;

    public AddPhotoRequestHandler(IRepository<Apartment> apartmentRepo,
        IBlobStorage blobStorage)
    {
        _apartmentRepo = apartmentRepo;
        _blobStorage = blobStorage;
    }

    public async Task<Response<string>> Handle(AddPhotoRequest request, CancellationToken cancellationToken)
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

        return response;
    }
}