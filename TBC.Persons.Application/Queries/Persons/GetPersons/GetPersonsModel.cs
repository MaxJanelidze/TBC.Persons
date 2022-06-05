using Shared.Application.PagedList;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Queries.Persons.GetPersons
{
    public class GetPersonsModel : SortedAndPagedListRequestBase
    {
        public string Search { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string PersonalNumber { get; set; }

        public Gender? Gender { get; set; }

        public int? CityId { get; set; }
    }
}
