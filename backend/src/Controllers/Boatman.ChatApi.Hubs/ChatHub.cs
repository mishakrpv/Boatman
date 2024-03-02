using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Boatman.ChatApi.Hubs;

[Authorize]
public class ChatHub : Hub<IHubClient>
{
    private readonly IMediator _mediator;
    
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override Task OnConnectedAsync()
    {
        return Task.CompletedTask;
    }
    
    public override Task OnDisconnectedAsync(Exception exception)
    {
        return Task.CompletedTask;
    }
}