using Boatman.FrontendApi.Owner.UseCases.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Owner.UseCases.Commands.AddApartment;

public class AddApartment : IRequest<Response<int>>
{
    public AddApartment(AddApartmentDto dto)
    {
        Dto = dto;
    }

    public AddApartmentDto Dto { get; private set; }
}