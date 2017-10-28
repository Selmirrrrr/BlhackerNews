namespace BlhackerNews.Models
{
    using System.Collections.Generic;
    using System.Linq;
    public class PagedList<T>
    {
        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public List<T> List { get; set; }
        public int TotalPages => (int)System.Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int NextPageNumber => HasNextPage ? PageNumber + 1 : TotalPages;
        public int PreviousPageNumber => HasPreviousPage ? PageNumber - 1 : 1;

        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            TotalItems = source.Count();
            PageNumber = pageNumber;
            PageSize = pageSize;
            List = source.Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize)
                         .ToList();
        }

        public PagingHeader GetHeader() => new PagingHeader(TotalItems, PageNumber, PageSize, TotalPages);
    }
}