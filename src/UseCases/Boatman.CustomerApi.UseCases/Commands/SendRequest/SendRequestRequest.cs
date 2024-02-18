using Boatman.CustomerApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.SendRequest;

public class SendRequestRequest : IRequest<Response>
{
    public SendRequestRequest(ApartmentCustomerDto dto)
    {
        Dto = dto;
    }

    public ApartmentCustomerDto Dto { get; private set; }
}