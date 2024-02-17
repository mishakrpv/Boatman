using Boatman.DataAccess.Domain.Interfaces;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.ScheduleViewing;

public class ScheduleViewingRequestHandler : IRequestHandler<ScheduleViewingRequest, Response>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public ScheduleViewingRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response> Handle(ScheduleViewingRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var apartment = await _apartmentRepo.GetByIdAsync(dto.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };

        var isSuccess = apartment.TryScheduleViewing(dto.CustomerId, dto.StartTime, dto.EndTime);

        if (!isSuccess)
            return new Response
            {
                StatusCode = 409,
                Message = "Viewing time conflicts with the past one."
            };
        
        await _apartmentRepo.UpdateAsync(apartment, cancellationToken);

        return new Response
        {
            Message = "Viewing has been scheduled."
        };
    }
}