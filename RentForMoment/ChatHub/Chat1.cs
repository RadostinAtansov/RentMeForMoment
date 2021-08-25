namespace RentForMoment.ChatHub
{

    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class Chat1 : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
