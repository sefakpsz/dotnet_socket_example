using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class DeathlyHallowHub : Hub
    {
        public static int Cloak { get; set; }
        public static int Wand { get; set; }
        public static int Stone { get; set; }
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