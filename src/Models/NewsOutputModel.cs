
namespace BlhackerNews.Models
{
    using System.Collections.Generic;
    
    public class NewsOutputModel
    {
        public PagingHeader Paging { get; set; }
        public List<NewsItem> Items { get; set; }
    }
 }