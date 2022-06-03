namespace Shared.Application.PagedList
{
    public class PagedListRequestBase
    {
        public int PageSize { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;
    }
}
