using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using TBC.Persons.Domain.Aggregates.Cities;
using TBC.Persons.Domain.Shared.ValueObjects;

namespace TBC.Persons.Domain.Aggregates.Persons
{
    public class Person : Entity<int>
    {
        private List<Phone> _phones;
        private List<PersonRelationship> _relatedPersons;
        private readonly List<PersonRelationship> _relatedPersonOf;

        public Person()
        {
            _phones = _phones ?? new List<Phone>();
            _relatedPersons = _relatedPersons ?? new List<PersonRelationship>();
            _relatedPersonOf = _relatedPersonOf ?? new List<PersonRelationship>();
        }

        public virtual MultiLanguageString Firstname { get; private set; }

        public virtual MultiLanguageString Lastname { get; private set; }

        public Gender Gender { get; private set; }

        public string PersonalNumber { get; private set; }

        public DateTime BirthDate { get; private set; }

        public int CityId { get; private set; }

        public string PictureFileAddress { get; private set; }

        public virtual IReadOnlyCollection<Phone> Phones => _phones;

        public virtual IReadOnlyCollection<PersonRelationship> RelatedPersons => _relatedPersons;

        public virtual IReadOnlyCollection<PersonRelationship> RelatedPersonOf => _relatedPersonOf;

        public virtual City City { get; private set; }

        public Person AssignName(MultiLanguageString firstname, MultiLanguageString lastname)
        {
            Firstname = firstname;
            Lastname = lastname;

            return this;
        }

        public Person AssignPersonalInformation(Gender gender, DateTime birthDate, string personalNumber)
        {
            Gender = gender;
            BirthDate = birthDate;
            PersonalNumber = personalNumber;

            return this;
        }

        public Person AssignContactInformation(params Phone[] phones)
        {
            if (Phones.Any())
            {
                _phones.Clear();
            }

            _phones.AddRange(phones);

            return this;
        }

        public Person AssignCity(City city)
        {
            City = city;

            return this;
        }

        public void AddPictureAddress(string address)
            => PictureFileAddress = address;

        public void AddRelatedPerson(Person relatedPerson, RelationType relationType)
        {
            _relatedPersons.Add(new PersonRelationship(relatedPerson, relationType));
        }

        public void RemoveRelatedPerson(Person relatedPerson)
        {
            if (RelatedPersons.Any())
            {
                _relatedPersons.RemoveAll(x => x.RelatedPersonId == relatedPerson.Id);
            }
        }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
