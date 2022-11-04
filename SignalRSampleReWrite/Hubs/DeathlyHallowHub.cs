using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class DeathlyHallowHub : Hub
    {
        public static int Cloak { get; set; } = 0;
        public static int Wand { get; set; } = 0;
        public static int Stone { get; set; } = 0;
        //public async Task IncreaseToCounter(string hallow)
        //{
        //    switch (hallow)
        //    {
        //        case "cloak":
        //            {
        //                Cloak++;
        //                break;
        //            }
        //        case "wand":                                                              SIGNALR DOESN'T WORK 
        //            {
        //                Wand++;
        //                break;
        //            }
        //        case "stone":
        //            {
        //                Stone++;
        //                break;
        //            }
        //        default:
        //            break;
        //    }
        //    await Clients.All.SendAsync("UpdateCounters", Cloak, Wand, Stone);
        //}

        public int CloakCounter()
        {
            return Cloak;
        }

        public int WandCounter()
        {
            return Wand;
        }

        public int StoneCounter()
        {
            return Stone;
        }
    }
}