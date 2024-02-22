using Boatman.FrontendApi.Owner.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.AddApartment;

public class AddApartmentRequest : IRequest<Response<int>>
{
    public AddApartmentRequest(AddApartmentDto dto)
    {
        Dto = dto;
    }

    public AddApartmentDto Dto { get; private set; }
}