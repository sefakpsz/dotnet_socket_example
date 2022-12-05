using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class Notification : Hub
    {
        private static int _counter = 0;
        private static string messages = "";
        public async Task MessageReceived(string input)
        {
            _counter++;
            messages += "Notification - " + input + " ";
            await Clients.All.SendAsync("MessageSent", messages, _counter);
        }

        //WHEN PAGE OPEN MESSAGES AND COUNTER WILL CONTINUE FROM WHERE IT WAS
    }
}