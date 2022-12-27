using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSampleReWrite.Data;

namespace SignalRSampleReWrite.Hubs
{
    [Authorize]
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasicChatHub(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public static List<string> _messages = new();
        public async Task MessageHandler(string message, string receiver, string sender)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (_dbContext.Users.Any(a => a.UserName == receiver) || string.IsNullOrEmpty(receiver))
                {
                    _messages.Add("from " + sender + " to " + (!string.IsNullOrEmpty(receiver) ? receiver : "all") + " \nMessage: " + message);
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
            }
        }

        public async Task<List<string>> GetAllMessages()
        {
            List<string> mssgs = new();
            foreach (var message in _messages)
            {
                var words = message.Split(" ");
                if (words[3] == "all" || words[3] == _httpContextAccessor.HttpContext.User.Identity.Name)
                {
                    mssgs.Add(message);
                }
            }
            return mssgs;
        }

    }
}