namespace BlhackerNews.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using BlhackerNews.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private NewsService _newsService;
        private readonly ILogger<NewsService> _logger;

        public NewsController(NewsService newsService, ILogger<NewsService> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopNews(string search = null)
        {
            try
            { 
                return Ok(await _newsService.GetLastNews(10));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when getting news.", ex);
                return new BadRequestResult();
            }
        }

    }
}
