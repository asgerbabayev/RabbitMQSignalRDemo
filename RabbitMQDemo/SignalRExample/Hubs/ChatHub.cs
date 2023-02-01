using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
