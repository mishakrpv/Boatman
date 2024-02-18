using Boatman.CustomerApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.AddToWishlist;

public class AddToWishlistRequest : IRequest<Response>
{
    public AddToWishlistRequest(AddToWishlistDto dto)
    {
        Dto = dto;
    }

    public AddToWishlistDto Dto { get; private set; }
}