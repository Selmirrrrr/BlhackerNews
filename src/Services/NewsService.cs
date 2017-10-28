namespace BlhackerNews.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using Microsoft.Extensions.Caching.Memory;

    public class NewsService
    {
        private IMemoryCache _memoryCache;
        private string _topNewsReq = "https://hacker-news.firebaseio.com/v0/topstories.json";
        private string _newsDetailssReq = $"https://hacker-news.firebaseio.com/v0/item/";

        public NewsService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        private async Task<List<int>> GetLastNewsIds(int nb = 10) 
            => JsonConvert.DeserializeObject<List<int>>(await HackerNewsApiRequest(_topNewsReq)).Take(nb).ToList();

        public async Task<NewsItem[]> GetLastNews(int nb = 10)
        {
            if (nb < 1) throw new ArgumentOutOfRangeException("Nb must be greater than 0");
            if (nb > 500) throw new ArgumentOutOfRangeException("Nb must be lower than 501");
            var list = await GetLastNewsIds(10);
            var tasks = list.AsParallel().Select(GetNewsItemDetails);
            return await Task.WhenAll(tasks);
        }

        public async Task<NewsItem> GetNewsItemDetails(int newsId) 
        {
            return await _memoryCache.GetOrCreateAsync(newsId, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(1);
                return JsonConvert.DeserializeObject<NewsItem>(await HackerNewsApiRequest(_newsDetailssReq + newsId + ".json"));
            });
        }

        private async Task<string> HackerNewsApiRequest(string request)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
