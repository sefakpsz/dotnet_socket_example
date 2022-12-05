using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class HarryPotterHouseHub : Hub
    {
        private static readonly List<string> _houseList = new();
        public async Task Subscribe(string house, bool unsubscribing)
        {
            if (!unsubscribing)
            {
                if (!_houseList.Contains(Context.ConnectionId + ":" + house))
                {
                    _houseList.Add(Context.ConnectionId + ":" + house);
                    await Groups.AddToGroupAsync(Context.ConnectionId, house);
                }
            }
            else
            {
                if (_houseList.Contains(Context.ConnectionId + ":" + house))
                {
                    _houseList.Remove(Context.ConnectionId + ":" + house);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, house);
                }
            }

            var houseList = "";
            foreach (var item in _houseList)
            {
                if (item.Contains(Context.ConnectionId))
                {
                    houseList += item.Split(":")[1] + " ";
                }
            }

            await Clients.Caller.SendAsync("Subbed", house, unsubscribing, "only", houseList);
            await Clients.Others.SendAsync("Subbed", house, unsubscribing, "everyone");
        }

        public async Task Triggering(string house)
        {
            await Clients.Group(house).SendAsync("Trigger");
        }
    }
}
//await Clients.All.SendAsync("ReceiveMessage", user, message);
//await Clients.Caller.SendAsync("ReceiveMessage", user, message);
//await Clients.Others.SendAsync("ReceiveMessage", user, message);
//await Clients.Client("Connection Id - A").SendAsync("ReceiveMessage", user, message);
//await Clients.Clients("Connection Id - A", "Connection Id - B").SendAsync("ReceiveMessage", user, message);
//await Clients.AllExcept("Connection Id - A", "Connection Id - C").SendAsync("ReceiveMessage", user, message);
//await Clients.User("ben@gmail.com").SendAsync("ReceiveMessage", user, message);
//await Clients.Users("ben@gmail.com", "jess@gmail.com").SendAsync("ReceiveMessage", user, message);
//await Groups.AddToGroupAsync(Context.ConnectionId, "GroupName");
//await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GroupName");
//await Clients.Group("admin").SendAsync("ReceiveMessage", user, message);
//await Clients.OthersInGroup("admin").SendAsync("ReceiveMessage", user, message);
//await Clients.Groups("admin", "user").SendAsync("ReceiveMessage", user, message);
//await Clients.GroupExcept("admin", "sam@gmail.com").SendAsync("ReceiveMessage", user, message);