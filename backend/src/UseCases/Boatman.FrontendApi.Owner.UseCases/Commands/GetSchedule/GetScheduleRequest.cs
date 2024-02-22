using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.GetSchedule;

public class GetScheduleRequest : IRequest<Response<IEnumerable<Viewing>>>
{
    public GetScheduleRequest(int apartmentId)
    {
        ApartmentId = apartmentId;
    }

    public int ApartmentId { get; private set; }
}