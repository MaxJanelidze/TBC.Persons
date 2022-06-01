using Shared.Domain;

namespace TBC.Persons.Domain.Aggregates.Persons
{
    public class RelatedPerson : Entity<int>
    {
        private RelatedPerson()
        {
        }

        public RelatedPerson(RelationType relationType, Person person)
            : this()
        {
            RelationType = relationType;
            Person = person;
        }

        public RelationType RelationType { get; private set; }

        public int PersonId { get; private set; }

        public virtual Person Person { get; private set; }
    }

    public enum RelationType
    {
        Relative = 1,
        Friend = 2,
        Colleague = 3,
        Other = 4
    }
}
