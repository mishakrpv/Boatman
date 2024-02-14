using Boatman.OwnerApi.UseCases.Dtos;
using Boatman.Utils;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.ScheduleViewing;

public class ScheduleViewingRequest : IRequest<Response>
{
    public ScheduleViewingRequest(PlanViewingDto dto)
    {
        Dto = dto;
    }

    public PlanViewingDto Dto { get; private set; }
}