using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.SubmitAnApplication;

public class SubmitAnApplicationRequestHandler : IRequestHandler<SubmitAnApplicationRequest, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public SubmitAnApplicationRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Response> Handle(SubmitAnApplicationRequest request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.SubmitAnApplication(request.CustomerId);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return new Response()
        {
            Message = "Request has been sent."
        };
    }
}