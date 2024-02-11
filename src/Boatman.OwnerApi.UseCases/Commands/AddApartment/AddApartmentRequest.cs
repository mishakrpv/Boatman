using Boatman.OwnerApi.UseCases.Dtos;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.AddApartment;

public class AddApartmentRequest : IRequest<int>
{
    public AddApartmentDto Dto { get; private set; }
    
    public AddApartmentRequest(AddApartmentDto dto)
    {
        Dto = dto;
    }
}