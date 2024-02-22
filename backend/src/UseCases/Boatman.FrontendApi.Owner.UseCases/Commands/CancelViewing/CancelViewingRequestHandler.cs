using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.CancelViewing;

public class CancelViewingRequestHandler : IRequestHandler<CancelViewingRequest, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public CancelViewingRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response> Handle(CancelViewingRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var apartment = await _apartmentRepo.GetByIdAsync(dto.ApartmentId, cancellationToken);
        
        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.CancelViewing(dto.ViewingId);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return new Response
        {
            Message = $"Viewing with id {dto.ViewingId} has been canceled."
        };
    }
}