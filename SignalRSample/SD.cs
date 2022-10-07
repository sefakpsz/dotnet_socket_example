namespace SignalRSample
{
    public static class SD
    {
        public static Dictionary<string, int> DeathlyHallowRace;

        public const string Wand = "wand";
        public const string Stone = "stone";
        public const string Cloak = "cloak";
        static SD()
        {
            DeathlyHallowRace = new Dictionary<string, int>
            {
                { Cloak, 0 },
                { Stone, 0 },
                { Wand, 0 }
            };
        }
    }
}