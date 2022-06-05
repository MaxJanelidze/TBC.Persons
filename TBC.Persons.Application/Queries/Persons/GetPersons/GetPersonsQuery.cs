using Shared.Application.Mediatr;
using Shared.Application.PagedList;
using Shared.Application.Pagination;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Queries.Persons.GetPersons
{
    public class GetPersonsQuery : SortedAndPagedListRequestBase, IQuery<PagedList<PersonsListItemModel>>
    {
        public string Search { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string PersonalNumber { get; set; }

        public Gender? Gender { get; set; }

        public int? CityId { get; set; }
    }
}
