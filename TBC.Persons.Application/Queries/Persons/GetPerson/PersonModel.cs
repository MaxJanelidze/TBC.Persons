﻿using System;
using System.Collections.Generic;
using TBC.Persons.Application.Shared.Models;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Queries.Persons.GetPerson
{
    public class PersonModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string PictureFileAddress { get; set; }

        public IEnumerable<Phone> Phones { get; set; }

        public IEnumerable<RelatedPerson> RelatedPersons { get; set; }
    }
}