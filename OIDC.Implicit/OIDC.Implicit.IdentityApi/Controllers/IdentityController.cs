using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OIDC.Implicit.IdentityApi.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        [HttpGet]
        [Route("api/identity")]
        [Authorize(Roles = "admin")]
        public object GetUserClaims()
        {
            return User.Claims.Select(r => new {r.Type, r.Value});
        }
    }
}