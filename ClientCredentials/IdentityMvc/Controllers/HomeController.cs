using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ClientCredentials.IdentityMvc.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> GetData()
        {
            var client = new HttpClient();
            
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
                return new JsonResult(new {err = disco.Error});
            
            var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                //获取Token的地址
                Address = disco.TokenEndpoint,
                //客户端Id
                ClientId = "apiClient",
                //客户端密码
                ClientSecret = "apiSecret",
                //要访问的api资源
                Scope = "secret_api.access"
            });
            
            if (token.IsError)
                return new JsonResult(new {err = token.Error});
            
            client.SetBearerToken(token.AccessToken);
            string data = await client.GetStringAsync("http://localhost:5001/api/identity");

            return new JsonResult(data);
        }
    }
}