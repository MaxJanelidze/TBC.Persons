using System;
using System.Collections.Generic;

namespace Shared.Application.Pagination
{
    public class PagedList<T>
    {
        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> data, int totalCount, int pageSize, int currentPage)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Data = data;
        }

        public int TotalCount { get; private set; }

        public int PageSize { get; private set; }

        public int CurrentPage { get; private set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Data { get; private set; }
    }
}
