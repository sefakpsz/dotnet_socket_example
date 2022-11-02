using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class UserCountHub : Hub
    {
        public static int ViewsCount { get; set; }
        public static int ConnectedUsers { get; set; }

        public override Task OnConnectedAsync()
        {
            ConnectedUsers++;
            Clients.All.SendAsync("UserConnection", ConnectedUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers--;
            Clients.All.SendAsync("UserConnection", ConnectedUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
        public async Task NewWindowLoaded()
        {
            ViewsCount++;
            await Clients.All.SendAsync("ReceiveNotification", ViewsCount);
        }
    }
}