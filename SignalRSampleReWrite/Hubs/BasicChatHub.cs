using Microsoft.AspNetCore.SignalR;
namespace SignalRSampleReWrite.Hubs
{


    public class BasicChatHub : Hub
    {
        public static List<string> _messages = new();
        public async Task MessageHandler(string message, string receiver, string sender)
        {

            _messages.Add("from " + sender + (!string.IsNullOrEmpty(receiver) ? " to " + receiver : "") + "\nMessage: " + message);
            await Clients.All.SendAsync("MessageReceived", _messages.Last());
        }

        public async Task<List<string>> GetAllMessages()
        {
            return _messages;
        }

    }
}