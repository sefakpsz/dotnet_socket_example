using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class HarryPotterHouseHub : Hub
    {
        public async Task Subscribe(string house, bool unOr)
        {
            await Clients.Caller.SendAsync("Subbed", house, unOr);
            await Clients.Others.SendAsync("Subbed", house, unOr);
        }
    }
}