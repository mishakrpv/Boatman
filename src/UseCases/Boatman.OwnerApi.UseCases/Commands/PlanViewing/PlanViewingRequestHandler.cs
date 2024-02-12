using Ardalis.GuardClauses;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.PlanViewing;

public class PlanViewingRequestHandler : IRequestHandler<PlanViewingRequest, bool>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public PlanViewingRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<bool> Handle(PlanViewingRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var apartment = await _apartmentRepo.GetByIdAsync(dto.ApartmentId, cancellationToken);
        Guard.Against.Null(apartment);
        
        var startTime = new ViewingDateTime(dto.StartTime.Year,
            dto.StartTime.Month, dto.StartTime.Day, dto.StartTime.Hour, dto.StartTime.Minute);
        var endTime = new ViewingDateTime(dto.EndTime.Year,
            dto.EndTime.Month, dto.EndTime.Day, dto.EndTime.Hour, dto.EndTime.Minute);
        
        bool isSuccess = apartment.TryPlanViewing(dto.CustomerId, startTime, endTime);
        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return isSuccess;
    }
}