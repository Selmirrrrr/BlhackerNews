namespace BlhackerNews.Services
{
    using Newtonsoft.Json;

    public class NewsItem 
    {
        [JsonProperty("kids")]
        public long[] Kids { get; set; }

        [JsonProperty("descendants")]
        public long Descendants { get; set; }

        [JsonProperty("by")]
        public string By { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}