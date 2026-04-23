using Microsoft.AspNetCore.SignalR;

namespace ZooManagmentSystem.Hubs
{
    public class GorillaHealthNot : Hub
    {
        public async Task SendGorillaHealthNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveGorillaHealthNotification", message);
        }
    }
}
