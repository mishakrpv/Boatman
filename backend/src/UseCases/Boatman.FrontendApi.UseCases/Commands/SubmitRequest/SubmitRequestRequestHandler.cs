using Boatman.DataAccess.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.SubmitRequest;

public class SubmitRequestRequestHandler : IRequestHandler<SubmitRequestRequest, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public SubmitRequestRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response> Handle(SubmitRequestRequest request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.SubmitRequest(request.CustomerId);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return new Response
        {
            Message = "Request has been sent."
        };
    }
}