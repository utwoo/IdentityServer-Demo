﻿using Microsoft.AspNetCore.Mvc;
using SecurityLevel.Security;

namespace SecurityLevel.Controllers
{
    public class WeatherForecastController : Controller
    {
        [HttpGet]
        [SecurityLevel(5)]
        public IActionResult Index()
        {
            return View();
        }
    }
}