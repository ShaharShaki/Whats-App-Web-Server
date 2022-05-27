using Microsoft.AspNetCore.SignalR;

namespace Liron_api.Hubs
{
    public class HubMessage : Hub
    {
        public async Task invokeSendMessage(string name, string message)
        {
            await Clients.All.SendAsync("messageValidation", name, message);
        }
    }
}