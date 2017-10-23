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

    public class NewsService
    {
        private HttpClient _client;

        public NewsService() {
            _client = new HttpClient();
        }

        public async Task<List<long>> GedottLastNews(int nb)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json");
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.ToString());
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<long>>(stringResult);
            }
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
