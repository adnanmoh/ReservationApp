using Microsoft.AspNetCore.SignalR;

namespace Reservation_APIs.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendMessageToUser(string receiverID, string message)
        {
            await Clients.User(receiverID).ReceiveMessage(message);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }




    }
}
