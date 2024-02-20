using Boatman.OwnerApi.UseCases.Dtos;
using Boatman.Utils;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.ScheduleViewing;

public class ScheduleViewingRequest : IRequest<Response>
{
    public ScheduleViewingRequest(ScheduleViewingDto dto)
    {
        Dto = dto;
    }

    public ScheduleViewingDto Dto { get; private set; }
}