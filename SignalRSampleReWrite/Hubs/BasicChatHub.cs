using Microsoft.AspNetCore.SignalR;
using SignalRSampleReWrite.Data;

namespace SignalRSampleReWrite.Hubs
{
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public BasicChatHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static List<string> _messages = new();
        public async Task MessageHandler(string message, string receiver, string sender)
        {

            _messages.Add("from " + sender + (!string.IsNullOrEmpty(receiver) ? " to " + receiver : "") + "\nMessage: " + message);
            if (!string.IsNullOrEmpty(receiver))
            {
                var bol = _dbContext.Users.FirstOrDefault(u => u.UserName == receiver);
                await Clients.User(bol.Id).SendAsync("MessageReceived", _messages.Last());
            }
            else
            {
                await Clients.All.SendAsync("MessageReceived", _messages.Last());
            }
        }

        public async Task<List<string>> GetAllMessages()
        {
            return _messages;
        }

    }
}