
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace tresure_api.Controllers.Hubs
{
    public class ProjectHub : Hub
    {
        public async Task SendProjectUpdate(string user, string projectUpdate)
        {
            await Clients.All.SendAsync("ReceiveProjectUpdate", user, projectUpdate);
        }
    }
}
