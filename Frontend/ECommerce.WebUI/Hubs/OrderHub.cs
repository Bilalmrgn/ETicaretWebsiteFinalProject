using Microsoft.AspNetCore.SignalR;

namespace ECommerce.WebUI.Hubs
{
    public class OrderHub : Hub
    {
        public async Task SendNewOrderNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNewOrderNotification", message);
        }
    }
}
