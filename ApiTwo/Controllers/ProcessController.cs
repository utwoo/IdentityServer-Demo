using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiTwo.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProcessController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> ClientCredentialTest()
        {
            var client = _httpClientFactory.CreateClient();

            // Request for Identity Server
            var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync("http://localhost:5100");
            if (discoveryDocumentResponse.IsError)
                return BadRequest("Discovery Documentation Error.");

            // Get AccessToken by Client Credential
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryDocumentResponse.TokenEndpoint,
                ClientId = "ApiTwo",
                ClientSecret = "APISecretKey",
                Scope = "api1.read"
            });
            if (tokenResponse.IsError)
                return BadRequest("Token Request Error.");
            
            // Request Secret Page in API #1 With Access Token
            client.SetBearerToken(tokenResponse.AccessToken);
            var httpResponseMessage = await client.GetAsync("http://localhost:5001/home/secret");

            return Ok(
                new
                {
                    tokenResponse.AccessToken,
                    Message = await httpResponseMessage.Content.ReadAsStringAsync(),
                });
        }
    }
}