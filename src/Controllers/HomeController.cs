namespace BlhackerNews.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using BlhackerNews.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private NewsService _newsService;

        public HomeController(NewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            return Ok(_newsService.GetLastNews(10));
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
