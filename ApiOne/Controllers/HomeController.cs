using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiOne.Controllers
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