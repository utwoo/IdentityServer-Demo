using Microsoft.AspNetCore.Mvc;

namespace SecurityLevel.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}