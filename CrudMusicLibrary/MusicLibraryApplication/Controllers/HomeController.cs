using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicLibraryApplication.Models;

namespace MusicLibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var cookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("cookieTest", out var cookieTest);

            ViewBag.CookieTest = cookieExists ? cookieTest : "Cookie not found!";

            var options = new CookieOptions();
            options.HttpOnly = true;
            options.Secure = true;
            options.SameSite = SameSiteMode.Strict;
            options.MaxAge = TimeSpan.FromMinutes(2);

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("cookieTest");
            _httpContextAccessor.HttpContext.Response.Cookies.Append("cookieTest", $"CookieTestValue [{Guid.NewGuid()}]", options);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
