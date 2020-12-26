using Microsoft.AspNetCore.Mvc;
using SecurityLevel.Security;

namespace SecurityLevel.Controllers
{
    public class SecurityController : Controller
    {
        [HttpGet]
        [SecurityLevel(1)]
        public IActionResult Level1()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(2)]
        public IActionResult Level2()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(3)]
        public IActionResult Level3()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(4)]
        public IActionResult Level4()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(5)]
        public IActionResult Level5()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(6)]
        public IActionResult Level6()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(7)]
        public IActionResult Level7()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(8)]
        public IActionResult Level8()
        {
            return View();
        }

        [HttpGet]
        [SecurityLevel(9)]
        public IActionResult Level9()
        {
            return View();
        }
        
        [HttpGet]
        [SecurityLevel(10)]
        public IActionResult Level10()
        {
            return View();
        }
    }
}