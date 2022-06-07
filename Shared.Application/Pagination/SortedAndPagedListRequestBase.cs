namespace Shared.Application.PagedList
{
    public abstract class SortedAndPagedListRequestBase : PagedListRequestBase
    {
        public string SortBy { get; set; }

        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }
}
