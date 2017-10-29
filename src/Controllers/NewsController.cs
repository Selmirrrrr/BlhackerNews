namespace BlhackerNews.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using BlhackerNews.Models;
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
        public async Task<IActionResult> GetTopNews(int pageNumber = 1, int pageSize = 10)
        {
            var model = await _newsService.GetNews(new PagingParams(pageNumber, pageSize));

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new NewsOutputModel
            {
                Paging = model.GetHeader(),
                Items = model.List.ToList(),
            };
            return Ok(outputModel);
        }
    }
}
