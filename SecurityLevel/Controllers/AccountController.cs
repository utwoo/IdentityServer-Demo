using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecurityLevel.Security;

namespace SecurityLevel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var claims = new List<Claim> {new Claim(Constants.SECURITY_LEVEL, "7")};
            var claimIdentity = new ClaimsIdentity(claims, "Custom Identity Type");
            var user = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(user);
            
            
            Console.WriteLine("User login successfully.");
            return Redirect(returnUrl);
        }
    }
}