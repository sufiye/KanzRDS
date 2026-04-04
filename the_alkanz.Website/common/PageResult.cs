namespace the_alkanz.Website.common;

public class PageResult<T>
{
 public IEnumerable<T> Items { get; set; } = new List<T>();

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPage
        => Convert.ToInt32(Math.Ceiling(TotalCount / (double)PageSize));
    public bool HasPrevious
        => Page > 1;
    public bool HasNext
        => Page < TotalPage;

    public static PageResult<T> Creat(
        IEnumerable<T> items,
        int page,
        int pagesize,
        int totalCount)
    {
        return new PageResult<T>
        {
            Items = items,
            Page = page,
            PageSize = pagesize,
            TotalCount = totalCount
        };
    }
}
