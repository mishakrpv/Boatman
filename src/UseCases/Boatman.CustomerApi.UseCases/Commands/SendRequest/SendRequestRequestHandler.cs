using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.SendRequest;

public class SendRequestRequestHandler : IRequestHandler<SendRequestRequest, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public SendRequestRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Response> Handle(SendRequestRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var apartment = await _apartmentRepo.GetByIdAsync(dto.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        apartment.SendRequest(dto.CustomerId);

        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return new Response()
        {
            Message = "Request has been sent."
        };
    }
}