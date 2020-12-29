using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok("Hello World!");
        }

        [Authorize]
        public IActionResult Secret()
        {
            return Ok("Secret Part");
        }
    }
}