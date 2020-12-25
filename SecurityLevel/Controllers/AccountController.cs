using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecurityLevel.Security;

namespace SecurityLevel.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string securityLevel)
        {
            var claims = new List<Claim> {new Claim(Constants.SECURITY_LEVEL, securityLevel)};
            var claimIdentity = new ClaimsIdentity(claims, "SecurityIdentityType");
            var user = new ClaimsPrincipal(claimIdentity);
            
            await HttpContext.SignInAsync(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}