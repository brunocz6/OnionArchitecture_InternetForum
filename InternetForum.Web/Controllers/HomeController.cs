using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternetForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            return View();
        }
    }
}
