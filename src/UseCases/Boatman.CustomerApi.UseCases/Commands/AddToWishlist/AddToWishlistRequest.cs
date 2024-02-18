using Boatman.CustomerApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.AddToWishlist;

public class AddToWishlistRequest : IRequest<Response>
{
    public AddToWishlistRequest(ApartmentCustomerDto dto)
    {
        Dto = dto;
    }

    public ApartmentCustomerDto Dto { get; private set; }
}