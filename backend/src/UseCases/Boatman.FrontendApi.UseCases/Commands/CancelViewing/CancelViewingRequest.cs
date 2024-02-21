using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.CancelViewing;

public class CancelViewingRequest : IRequest<Response>
{
    public CancelViewingRequest(CancelViewingDto dto)
    {
        Dto = dto;
    }

    public CancelViewingDto Dto { get; private set; }
}