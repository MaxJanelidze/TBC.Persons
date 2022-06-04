using MediatR;
using Shared.Application.Mediatr;
using System;
using System.Collections.Generic;
using TBC.Persons.Application.Commands.Persons.Shared;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Commands.Persons.Update
{
    public class ChangePersonDetailsCommand : ICommand<Unit>
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public IEnumerable<Phone> Phones { get; set; }
    }
}
