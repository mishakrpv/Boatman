using Boatman.CommonApi.Hubs.Contracts;
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
        await Clients.Caller.ReceiveMessage("You just joined");
    }
    
    public async Task SendMessage(string userConnectionId, string message)
    {
        await Clients.Caller.ReceiveMessage(message);
        await Clients.Client(userConnectionId).ReceiveMessage(message);
    }
}