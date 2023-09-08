using Alpaca.Portal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alpaca.Portal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy");
            return View();
        }

        [ActionName("User")]
        public IActionResult UserPage()
        {
            _logger.LogInformation("User");
            return View();
        }
        
        public IActionResult Notices()
        {
            _logger.LogInformation("お知らせページ");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}