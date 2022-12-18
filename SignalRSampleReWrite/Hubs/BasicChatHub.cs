using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class BasicChatHub : Hub
    {
        public static Dictionary<string, string> Messages = new Dictionary<string, string>();
        public async Task MessageHandler(string message, string receiver, string sender)
        {
            Messages.Add(sender, message);
            string messages = "";
            foreach (var item in Messages)
            {
                messages += item.Key + " - " + item.Value;
            }
            if (string.IsNullOrEmpty(receiver))
            {
                await Clients.All.SendAsync("MessageReceived", messages, Messages.Count);
            }
            else
            {
                await Clients.User(receiver).SendAsync("MessageReceived", messages, Messages.Count);
            }
        }
    }
}