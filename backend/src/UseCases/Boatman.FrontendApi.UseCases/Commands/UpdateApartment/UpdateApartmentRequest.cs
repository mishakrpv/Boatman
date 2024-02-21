using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.UpdateApartment;

public class UpdateApartmentRequest : IRequest<Response>
{
    public UpdateApartmentRequest(UpdateApartmentDto dto)
    {
        Dto = dto;
    }

    public UpdateApartmentDto Dto { get; private set; }
}