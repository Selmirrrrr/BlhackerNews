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

    public class NewsService
    {
        private string _topNewsReq = "https://hacker-news.firebaseio.com/v0/topstories.json";
        private string _newsDetailssReq = $"https://hacker-news.firebaseio.com/v0/item/";

        private async Task<List<int>> GetLastNewsIds(int nb) 
            => JsonConvert.DeserializeObject<List<int>>(await HackerNewsApiRequest(_topNewsReq)).Take(nb).ToList();

        public async Task<List<NewsItem>> GetLastNews(int nb)
        {
            if (nb < 1) throw new ArgumentOutOfRangeException("Nb must be greater than 0");
            if (nb > 500) throw new ArgumentOutOfRangeException("Nb must be lower than 501");
            var list = await GetLastNewsIds(nb);
            var tasks = list.Select(GetNewsItemDetails);
            return Task.WhenAll(tasks).Result.ToList();
        }

        public async Task<NewsItem> GetNewsItemDetails(int newsId) 
            => JsonConvert.DeserializeObject<NewsItem>(await HackerNewsApiRequest(_newsDetailssReq + newsId + ".json"));

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
