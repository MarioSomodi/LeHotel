namespace LeHotel.Contracts.Common
{
    public record PagedResultResponse<T>(IEnumerable<T> Records, int CurrentPage, int PageSize, int TotalRecords, int TotalPages, bool HasNextPage, bool HasPreviousPage, int? NextPage, int? PreviousPage);
}
