using Shared.Domain;

namespace TBC.Persons.Domain.Aggregates.Persons
{
    public class PersonRelationship : Entity<int>
    {
        protected PersonRelationship()
        {
        }

        public PersonRelationship(/*Person masterPerson,*/ Person relatedPerson, RelationType relationType)
            : this()
        {
            //MasterPerson = masterPerson;
            RelatedPersonId = relatedPerson.Id;
            RelationType = relationType;
        }

        public RelationType RelationType { get; private set; }

        public int MasterPersonId { get; private set; }

        public int RelatedPersonId { get; private set; }

        public virtual Person MasterPerson { get; private set; }

        public virtual Person RelatedPerson { get; set; }
    }

    public enum RelationType
    {
        Relative = 1,
        Friend = 2,
        Colleague = 3,
        Other = 4
    }
}
