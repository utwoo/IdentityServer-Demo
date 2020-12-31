using Microsoft.AspNetCore.Mvc;
using OIDC.IdentityMvc.Models;

namespace OIDC.IdentityMvc.Controllers
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