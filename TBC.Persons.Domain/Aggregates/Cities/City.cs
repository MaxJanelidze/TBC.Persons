using Shared.Domain;
using TBC.Persons.Domain.Shared.ValueObjects;

namespace TBC.Persons.Domain.Aggregates.Cities
{
    public class City : Entity<int>
    {
        private City()
        {
        }

        public City(int id, MultiLanguageString name)
        {
            Id = id;
            Name = name;
        }

        public virtual MultiLanguageString Name { get; private set; }
    }
}
