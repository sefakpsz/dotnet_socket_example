using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class HarryPotterHouseHub : Hub
    {
        public static List<string> HouseList { get; set; } = new List<string>();
        public async Task Subscribe(string house, bool unsubscribing)
        {
            if (unsubscribing)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, house);
                foreach (var item in HouseList)
                {
                    if (item.Equals(house))
                        HouseList.Remove(house);
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, house);
                foreach (var item in HouseList)
                {
                    if (!item.Equals(house))
                        HouseList.Add(house);
                }
            }

            var houseList = "";
            foreach (var item in HouseList)
            {
                houseList += item + " ";
            }

            await Clients.Caller.SendAsync("Subbed", house, unsubscribing, "only", houseList);
            await Clients.Others.SendAsync("Subbed", house, unsubscribing, "everyone", houseList);
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