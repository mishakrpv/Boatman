using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.DeleteApartment;

public class DeleteApartmentRequestHandler : IRequestHandler<DeleteApartmentRequest, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public DeleteApartmentRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<Response> Handle(DeleteApartmentRequest request, CancellationToken cancellationToken)
    {
        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response()
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        await _apartmentRepo.DeleteAsync(apartment, cancellationToken);

        return new Response
        {
            Message = "Apartment has been deleted."
        };
    }
}