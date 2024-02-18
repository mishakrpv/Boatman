using System.Security.Claims;
using Boatman.CommonApi.Hubs.Schemes.SendSchemes;
using Boatman.SharedApi.Hubs.Contracts;
using Boatman.SharedApi.Hubs.Schemes.ReceiveSchemes;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Boatman.SharedApi.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub<IHubClient>
{
    private static readonly Dictionary<string, string> Connections = new();
    private readonly IMediator _mediator;
    
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override Task OnConnectedAsync()
    {
        var emailClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        if (emailClaim != null)
        {
            Connections.Add(emailClaim.Value, Context.ConnectionId);
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var emailClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        if (emailClaim != null)
        {
            Connections.Remove(emailClaim.Value);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(SendMessageScheme scheme)
    {
        if (Connections.TryGetValue(scheme.ToEmail, out var connectionId))
        {
            await Clients.Client(connectionId).ReceiveMessage(scheme.Message);
            await Notify(connectionId);
        }
    }

    public async Task Notify(string connectionId)
    {
        var emailClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        if (emailClaim != null)
        {
            await Clients.Client(connectionId).ReceiveNotification(new ReceiveNotificationScheme
            {
                SenderEmail = emailClaim.Value
            });
        }
    }
}