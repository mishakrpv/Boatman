using Boatman.Caching.Interfaces;
using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Extensions;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.UpdateApartment;

public class UpdateApartmentHandler : IRequestHandler<UpdateApartment, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;
    private readonly ICache _cache;

    public UpdateApartmentHandler(IRepository<Apartment> apartmentRepo,
        ICache cache)
    {
        _apartmentRepo = apartmentRepo;
        _cache = cache;
    }
    
    public async Task<Response> Handle(UpdateApartment request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var spec = new ApartmentWithPhotosSpecification(dto.ApartmentId);
        var apartment = await _apartmentRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.Update(dto.Rent, dto.Description, dto.DownPaymentInMonths);
        apartment.SetCoordinates(dto.Latitude, dto.Longitude);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);
        
        await _cache.SetAsync<Apartment>(CacheHelpers.GenerateApartmentCacheKey(apartment.Id), apartment);

        return new Response
        {
            Message = "Apartment was updated."
        };
    }
}