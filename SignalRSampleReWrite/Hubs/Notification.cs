using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class Notification : Hub
    {
        public static int _counter = 0;
        public static List<string> _messages = new();
        public async Task MessageReceived(string input)
        {
            _counter++;
            _messages.Add("Notification - " + input + " ");
            await MessagesAndCounter();
        }

        public async Task MessagesAndCounter()
        {
            await Clients.All.SendAsync("LoadMessages", _counter, _messages);
        }
    }
}