using Microsoft.AspNetCore.SignalR;

namespace SignalRSampleReWrite.Hubs
{
    public class BasicChatHub : Hub
    {
        public static string messages = "";
        public async Task MessageHandler(string message, string receiver, string sender)
        {
            messages = sender + " - " + message;

            if (string.IsNullOrEmpty(receiver))
                await Clients.All.SendAsync("MessageReceived", messages);
            else
                await Clients.User(receiver).SendAsync("MessageReceived", messages);
        }

        public async Task<string> ReadMessages()
        {
            return messages;
        }
    }
}