namespace Shared.Application.PagedList
{
    public abstract class PagedListRequestBase
    {
        public int PageSize { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;
    }
}
