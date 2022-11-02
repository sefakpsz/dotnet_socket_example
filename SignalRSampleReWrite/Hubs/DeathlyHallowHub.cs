using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class DeathlyHallowHub : Hub
    {
        public static int Cloak { get; set; }
        public static int Wand { get; set; }
        public static int Stone { get; set; }
        public async Task IncreaseToCounter(string hallow)
        {
            switch (hallow)
            {
                case "cloak":
                    {
                        Cloak++;
                        break;
                    }
                case "wand":
                    {
                        Wand++;
                        break;
                    }
                case "stone":
                    {
                        Stone++;
                        break;
                    }
                default:
                    break;
            }
            await Clients.All.SendAsync("UpdateCounters", Cloak, Wand, Stone);
        }
    }
}