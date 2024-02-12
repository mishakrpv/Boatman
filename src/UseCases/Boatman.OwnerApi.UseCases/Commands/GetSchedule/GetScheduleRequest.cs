using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.GetSchedule;

public class GetScheduleRequest : IRequest<IEnumerable<Viewing>>
{
    public int ApartmentId { get; private set; }

    public GetScheduleRequest(int apartmentId)
    {
        ApartmentId = apartmentId;
    }
}