using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSampleReWrite.Hubs;
using SignalRSampleReWrite.Models;
using System.Diagnostics;

namespace SignalRSampleReWrite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowHub> _deathlyHallowHub;
        public static int Cloak { get; set; }
        public static int Wand { get; set; }
        public static int Stone { get; set; }

        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowHub> deathlyHallowHub)
        {
            _logger = logger;
            _deathlyHallowHub = deathlyHallowHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeathlyHallowRace()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string hallow)
        {
            await _deathlyHallowHub.Clients.All.SendAsync("Vote", hallow);
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}