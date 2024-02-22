using Boatman.FrontendApi.Owner.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.UpdateApartment;

public class UpdateApartmentRequest : IRequest<Response>
{
    public UpdateApartmentRequest(UpdateApartmentDto dto)
    {
        Dto = dto;
    }

    public UpdateApartmentDto Dto { get; private set; }
}