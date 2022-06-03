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

        public virtual MultiLanguageString Firstname { get; private set; }

        public virtual MultiLanguageString Lastname { get; private set; }

        public Gender Gender { get; private set; }

        public string PersonalNumber { get; private set; }

        public DateTime BirthDate { get; private set; }

        public int CityId { get; private set; }

        public string PictureFileAddress { get; private set; }

        public virtual IReadOnlyCollection<Phone> Phones => _phones;

        public virtual IReadOnlyCollection<RelatedPerson> RelatedPersons => _relatedPersons;

        public virtual City City { get; private set; }

        public Person AddPersonalInformation(Gender gender, DateTime birthDate, string personalNumber)
        {
            Gender = gender;
            BirthDate = birthDate;
            PersonalNumber = personalNumber;

            return this;
        }

        public Person AddContactInformation(params Phone[] phones)
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

        public void AddRelatedPerson(params RelatedPerson[] relatedPersons)
        {
            if (RelatedPersons.Any())
            {
                _relatedPersons.Clear();
            }

            _relatedPersons.AddRange(relatedPersons);
        }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
