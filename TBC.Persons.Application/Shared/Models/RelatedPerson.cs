using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Shared.Models
{
    public class RelatedPerson
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public RelationType RelationType { get; set; }
    }
}
