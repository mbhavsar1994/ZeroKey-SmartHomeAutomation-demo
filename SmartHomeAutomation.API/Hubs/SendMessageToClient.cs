using Microsoft.AspNetCore.SignalR;

namespace SmartHomeAutomation.API.Hubs;

public class NotificationHub : Hub
{
    public async Task SendMessageToClient(string deviceId, string message)
    {
        await Clients.User(deviceId).SendAsync("ReceiveMessage", message);
    }
}