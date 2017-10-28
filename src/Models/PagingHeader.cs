
namespace BlhackerNews.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class PagingHeader
    {
        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public PagingHeader(int totalItems, int pageNumber, int pageSize, int totalPages)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public string ToJson() => JsonConvert.SerializeObject(this, new JsonSerializerSettings { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });
    }
}