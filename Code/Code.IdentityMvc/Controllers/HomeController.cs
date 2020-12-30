using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Code.IdentityMvc.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> GetData(string code)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5000");
            if (disco.IsError)
                return new JsonResult(new {err = disco.Error});

            var token = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "apiClient",
                ClientSecret = "apiSecret",
                Code = code,
                RedirectUri = "https://localhost:5002/auth.html"
            });

            if (token.IsError)
                return new JsonResult(new {err = token.Error});

            client.SetBearerToken(token.AccessToken);
            string data = await client.GetStringAsync("https://localhost:5001/api/identity");

            return new JsonResult(data);
        }
    }
}