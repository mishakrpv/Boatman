using Boatman.SharedApi.UseCases.Dtos;
using MediatR;

namespace Boatman.SharedApi.UseCases.Commands.StartChat;

public class StartChatRequest : IRequest
{
    public StartChatRequest(StartChatDto dto)
    {
        Dto = dto;
    }

    public StartChatDto Dto { get; private set; }
}