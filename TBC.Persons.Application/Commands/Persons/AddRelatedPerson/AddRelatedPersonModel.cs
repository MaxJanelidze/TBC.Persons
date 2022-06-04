using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Commands.Persons.AddRelatedPerson
{
    public class AddRelatedPersonModel
    {
        public int MastertPersonId { get; set; }

        public int RelatedPersonId { get; set; }

        public RelationType RelationType { get; set; }
    }
}
