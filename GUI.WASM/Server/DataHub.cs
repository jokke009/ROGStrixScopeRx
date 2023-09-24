using Microsoft.AspNetCore.SignalR;

internal class DataHub : Hub
{
    public async Task SendMessage(string user, string message)
    => await Clients.All.SendAsync("ReceiveMessage", user, message);
}