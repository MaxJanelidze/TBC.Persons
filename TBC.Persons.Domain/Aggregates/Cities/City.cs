using Shared.Domain;
using TBC.Persons.Domain.Shared.ValueObjects;

namespace TBC.Persons.Domain.Aggregates.Cities
{
    public class City : Entity<int>
    {
        protected City()
        {
        }

        public City(MultiLanguageString name)
        {
            Name = name;
        }

        public virtual MultiLanguageString Name { get; private set; }
    }
}
