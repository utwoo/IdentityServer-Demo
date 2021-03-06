﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut(
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }

        [Authorize]
        public async Task<IActionResult> Detail()
        {
            var client = new HttpClient();  
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            client.SetBearerToken(accessToken);
            Console.WriteLine($"ACCESS_TOKEN: {accessToken}");
            Console.WriteLine($"REFRESH_TOKEN: {refreshToken}");
            string data = await client.GetStringAsync("https://localhost:5001/api/identity");
            JArray json = JArray.Parse(data);
            return new JsonResult(json);
        }
    }
}