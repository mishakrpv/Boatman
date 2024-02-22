using Boatman.FrontendApi.Owner.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.ScheduleViewing;

public class ScheduleViewingRequest : IRequest<Response>
{
    public ScheduleViewingRequest(ScheduleViewingDto dto)
    {
        Dto = dto;
    }

    public ScheduleViewingDto Dto { get; private set; }
}