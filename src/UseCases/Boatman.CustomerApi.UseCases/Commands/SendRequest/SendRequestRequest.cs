using Boatman.CustomerApi.UseCases.Dtos;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.SendRequest;

public class SendRequestRequest : IRequest<Response>
{
    public SendRequestRequest(SendRequestDto dto)
    {
        Dto = dto;
    }

    public SendRequestDto Dto { get; private set; }
}