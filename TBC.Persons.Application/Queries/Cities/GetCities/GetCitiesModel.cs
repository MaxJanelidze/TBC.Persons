using Shared.Application.PagedList;

namespace TBC.Persons.Application.Queries.Cities.GetCities
{
    public class GetCitiesModel : SortedAndPagedListRequestBase
    {
        public string Name { get; set; }
    }
}
