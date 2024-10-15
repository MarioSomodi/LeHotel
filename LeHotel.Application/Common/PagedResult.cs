namespace LeHotel.Application.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }

        public PagedResult(IEnumerable<T> records, int currentPage, int pageSize, int totalRecords)
        {
            Records = records;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            HasPreviousPage = currentPage > 1;
            HasNextPage = currentPage < TotalPages;
            PreviousPage = HasPreviousPage ? currentPage - 1 : null;
            NextPage = HasNextPage ? currentPage + 1 : null;
        }
    }

}
