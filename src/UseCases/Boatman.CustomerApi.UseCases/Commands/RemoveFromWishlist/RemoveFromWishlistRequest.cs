using Boatman.CustomerApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.RemoveFromWishlist;

public class RemoveFromWishlistRequest : IRequest<Response>
{
    public RemoveFromWishlistRequest(ApartmentCustomerDto dto)
    {
        Dto = dto;
    }
    
    public ApartmentCustomerDto Dto { get; private set; }
}