using Microsoft.AspNetCore.SignalR;

namespace Whats_App_Web_Server.Hubs


{
    public class HubMessage : Hub
    {
        public async Task invokeSendMessage(string name, string message)
        {
            await Clients.All.SendAsync("messageValidation" ,name, message);
        }

    }


}
