using Boatman.FrontendApi.Owner.UseCases.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.UpdateApartment;

public class UpdateApartment : IRequest<Response>
{
    public UpdateApartment(UpdateApartmentDto dto)
    {
        Dto = dto;
    }

    public UpdateApartmentDto Dto { get; private set; }
}