public class PaginatedResult<T>
{
    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public List<T> Data { get; set; } = new List<T>();


}