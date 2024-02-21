using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.AddApartment;

public class AddApartmentRequest : IRequest<Response<int>>
{
    public AddApartmentRequest(AddApartmentDto dto)
    {
        Dto = dto;
    }

    public AddApartmentDto Dto { get; private set; }
}