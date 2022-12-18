using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSampleReWrite.Data;
using SignalRSampleReWrite.Hubs;
using SignalRSampleReWrite.Models;
using System.Diagnostics;

namespace SignalRSampleReWrite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowHub> _deathlyHallowHub;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowHub> deathlyHallowHub, ApplicationDbContext contex, IHubContext<OrderHub> orderHub)
        {
            _logger = logger;
            _deathlyHallowHub = deathlyHallowHub;
            _context = contex;
            _orderHub = orderHub;
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
            switch (hallow.ToLower())
            {
                case "stone":
                    DeathlyHallowHub.Stone++;
                    break;
                case "wand":
                    DeathlyHallowHub.Wand++;
                    break;
                case "cloak":
                    DeathlyHallowHub.Cloak++;
                    break;
            }
            await _deathlyHallowHub.Clients.All.SendAsync("UpdateCounters", DeathlyHallowHub.Cloak, DeathlyHallowHub.Stone, DeathlyHallowHub.Wand);
            return Accepted();
        }

        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random rand = new();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index == 0 ? 1 : index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            await _orderHub.Clients.All.SendAsync("UpdateOrderList");
            return RedirectToAction(nameof(Order));
        }
        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }

        public async Task<IActionResult> HarryPotterHouse()
        {
            return View();
        }

        public async Task<IActionResult> Notification()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = _context.Orders.ToList();
            return Json(new { data = productList });
        }

        public async Task<IActionResult> BasicChat()
        {
            return View();
        }
    }
}