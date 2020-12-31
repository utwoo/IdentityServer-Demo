using Microsoft.AspNetCore.Mvc;
using OIDC.Implicit.IdentityMvc.Models;

namespace OIDC.Implicit.IdentityMvc.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult GetTokenData(TokenData data)
        {
            return new JsonResult(data);
        }
    }
}