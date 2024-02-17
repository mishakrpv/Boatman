using Boatman.CommonApi.Hubs.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Boatman.CommonApi.Hubs.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub<IHubClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveMessage("U just joined");
    }
    
    public async Task SendMessage(string userConnectionId, string message)
    {
        await Clients.Caller.ReceiveMessage(message);
        await Clients.Client(userConnectionId).ReceiveMessage(message);
    }
}