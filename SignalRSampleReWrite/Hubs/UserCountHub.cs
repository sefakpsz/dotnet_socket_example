using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class UserCountHub : Hub
    {
        private int ViewsCount;
        private int ConnectedUsers;

        public UserCountHub(int userCount, int connectedUsers)
        {
            ViewsCount = userCount;
            ConnectedUsers = connectedUsers;
        }

        public async Task SendNotification()
        {
            ViewsCount++;
            await Clients.All.SendAsync("ReceiveNotification", ViewsCount);
        }

        public override async Task OnConnectedAsync()
        {
            ConnectedUsers++;
            await Clients.All.SendAsync("UserConnected", ConnectedUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers--;
            await Clients.All.SendAsync("UserDisconnected", ConnectedUsers);
        }
    }
}