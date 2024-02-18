using Boatman.SharedApi.UseCases.Dtos;
using MediatR;

namespace Boatman.SharedApi.UseCases.Commands.SendMessage;

public class SendMessageRequest : IRequest
{
    public SendMessageRequest(SendMessageDto dto)
    {
        Dto = dto;
    }

    public SendMessageDto Dto { get; private set; }
}