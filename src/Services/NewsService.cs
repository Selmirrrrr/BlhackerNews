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

        public async Task GetLastNews(int nb)
        {
            using (var client = new HttpClient())
            {
                try
                {   
                    client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0");
                    var response = await client.GetAsync($"/topstories.json");
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine(response.ToString());
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<List<long>>(stringResult);
                }
                catch (HttpRequestException httpRequestException)
                {
                    // return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
