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
        public IActionResult Index() => View();

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
