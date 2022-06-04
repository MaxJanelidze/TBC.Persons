using MediatR;
using Shared.Application.Mediatr;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Commands.Persons.AddRelatedPerson
{
    public class AddRelatedPersonCommand : ICommand<Unit>
    {
        public int MastertPersonId { get; set; }

        public int RelatedPersonId { get; set; }

        public RelationType RelationType { get; set; }
    }
}
