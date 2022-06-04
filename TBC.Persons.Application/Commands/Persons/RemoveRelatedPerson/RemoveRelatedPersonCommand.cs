using MediatR;
using Shared.Application.Mediatr;

namespace TBC.Persons.Application.Commands.Persons.RemoveRelatedPerson
{
    public class RemoveRelatedPersonCommand : ICommand<Unit>
    {
        public int MastertPersonId { get; set; }

        public int RelatedPersonId { get; set; }
    }
}
