using Shared.Domain;
using System;
using System.Collections.Generic;
using TBC.Persons.Domain.Aggregates.Cities;
using TBC.Persons.Domain.Shared.ValueObjects;

namespace TBC.Persons.Domain.Aggregates.Persons
{
    public class Person : Entity<int>
    {
        private List<Phone> _phones;
        private List<RelatedPerson> _relatedPersons;

        private Person()
        {
            _phones = _phones ?? new List<Phone>();
            _relatedPersons = _relatedPersons ?? new List<RelatedPerson>();
        }

        public Person(MultiLanguageString firstname, MultiLanguageString lastname)
            : this()
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public virtual MultiLanguageString Firstname { get; set; }

        public virtual MultiLanguageString Lastname { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string PictureFileAddress { get; set; }

        public virtual IReadOnlyCollection<Phone> Phones => _phones;

        public virtual IReadOnlyCollection<RelatedPerson> RelatedPersons => _relatedPersons;

        public virtual City City { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
