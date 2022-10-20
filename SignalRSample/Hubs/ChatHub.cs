using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;
using System.Security.Claims;

namespace SignalRSample.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public override Task OnConnectedAsync()
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!String.IsNullOrEmpty(UserId))
            {
                var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserConnected", UserId, userName);
                HubConnections.AddUserConnection(UserId, Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (HubConnections.HasUserConnection(UserId, Context.ConnectionId))
            {
                var userConnections = HubConnections.Users[UserId];
                userConnections.Remove(Context.ConnectionId);

                HubConnections.Users.Remove(UserId);
                if (userConnections.Any())
                    HubConnections.Users.Add(UserId, userConnections);
            }

            if (!String.IsNullOrEmpty(UserId))
            {
                var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserDisconnected", UserId, userName);
                HubConnections.AddUserConnection(UserId, Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAddRoomMessage(int maxRoom, int roomId, string roomName)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;
            await Clients.All.SendAsync("ReceiveAddRoomMessage", maxRoom, roomId, roomName, UserId, userName);
        }

        public async Task SendDeleteRoomMessage(int deleted, int selected, string roomName)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;
            await Clients.All.SendAsync("ReceiveDeleteRoomMessage", deleted, selected, roomName, userName);
        }

        public async Task SendPublicMessage(int roomId, string message, string roomName)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;

            await Clients.All.SendAsync("ReceivePublicMessage", roomId, UserId, userName, message, roomName);
        }
    }
}