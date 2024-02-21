using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.ScheduleViewing;

public class ScheduleViewingRequest : IRequest<Response>
{
    public ScheduleViewingRequest(ScheduleViewingDto dto)
    {
        Dto = dto;
    }

    public ScheduleViewingDto Dto { get; private set; }
}