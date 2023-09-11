using Microsoft.AspNetCore.Mvc;

namespace Alpaca.Portal.Web.Controllers
{
    public class DailyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
