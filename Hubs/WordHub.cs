using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WordApi.Hubs
{
    public class WordHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
