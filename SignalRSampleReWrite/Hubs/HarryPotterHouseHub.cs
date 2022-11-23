using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class HarryPotterHouseHub : Hub
    {
        private readonly object user;
        private readonly object message;
        public async Task Subscribing(string house, bool unsubscribing)
        {
            
            await Clients.Caller.SendAsync("Subbed", house);
            await Clients.Others.SendAsync("Subbed", house);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.Send Async("ReceiveMessage", user, message);
            await Clients.Others.SendAsync("ReceiveMessage", user, message);
            await Clients.Client("Connection Id - A").SendAsync("ReceiveMessage", user, message);
            await Clients.Clients("Connection Id - A", "Connection Id - B").SendAsync("ReceiveMessage", user, message);
            await Clients.AllExcept("Connection Id - A", "Connection Id - C").SendAsync("ReceiveMessage", user, message);
            await Clients.User("ben@gmail.com").SendAsync("ReceiveMessage", user, message);
            await Clients.Users("ben@gmail.com", "jess@gmail.com").SendAsync("ReceiveMessage", user, message);
            await Groups.AddToGroupAsync(Context.ConnectionId, "GroupName");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GroupName");
            await Clients.Group("admin").SendAsync("ReceiveMessage", user, message);
            await Clients.OthersInGroup("admin").SendAsync("ReceiveMessage", user, message);
            await Clients.Groups("admin", "user").SendAsync("ReceiveMessage", user, message);
            await Clients.GroupExcept("admin", "sam@gmail.com").SendAsync("ReceiveMessage", user, message);
        }
    }
}