using Boatman.OwnerApi.UseCases.Dtos;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.PlanViewing;

public class PlanViewingRequest : IRequest<bool>
{
    public PlanViewingDto Dto { get; private set; }

    public PlanViewingRequest(PlanViewingDto dto)
    {
        Dto = dto;
    }
}