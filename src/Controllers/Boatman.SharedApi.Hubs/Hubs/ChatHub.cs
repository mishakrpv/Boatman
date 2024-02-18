using Boatman.CommonApi.Hubs.Contracts;
using Boatman.SharedApi.UseCases.Commands.StartChat;
using Boatman.SharedApi.UseCases.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Boatman.CommonApi.Hubs.Hubs;

// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub<IHubClient>
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        
    }

    public async Task StartChat(string userConnectionId, StartChatDto dto)
    {
        await Clients.Caller.ReceiveMessage(dto.Message);
        await Clients.Client(userConnectionId).ReceiveMessage(dto.Message);
        await _mediator.Send(new StartChatRequest(dto));
    }
    
    public async Task SendMessage(string userConnectionId, string message)
    {
        await Clients.Caller.ReceiveMessage(message);
        await Clients.Client(userConnectionId).ReceiveMessage(message);
    }
}