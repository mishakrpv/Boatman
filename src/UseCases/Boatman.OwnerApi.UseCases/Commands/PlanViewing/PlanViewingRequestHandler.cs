using Ardalis.GuardClauses;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.PlanViewing;

public class PlanViewingRequestHandler : IRequestHandler<PlanViewingRequest>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public PlanViewingRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task Handle(PlanViewingRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var apartment = await _apartmentRepo.GetByIdAsync(dto.ApartmentId, cancellationToken);
        Guard.Against.Null(apartment);
        apartment.PlanViewing(dto.CustomerId, dto.StartTime, dto.EndTime);
        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);
    }
}