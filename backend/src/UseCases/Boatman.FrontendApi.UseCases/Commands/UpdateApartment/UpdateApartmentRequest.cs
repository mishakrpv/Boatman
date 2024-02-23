using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.UpdateApartment;

public class UpdateApartmentRequest : IRequest<Response>
{
    public UpdateApartmentRequest(UpdateApartmentDto dto)
    {
        Dto = dto;
    }

    public UpdateApartmentDto Dto { get; private set; }
}