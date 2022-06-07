using Shared.Application.Mediatr;
using Shared.Application.PagedList;
using Shared.Application.Pagination;

namespace TBC.Persons.Application.Queries.Cities.GetCities
{
    public class GetCitiesQuery : SortedAndPagedListRequestBase, IQuery<PagedList<CitiesListItemModel>>
    {
        public string Name { get; set; }
    }
}
