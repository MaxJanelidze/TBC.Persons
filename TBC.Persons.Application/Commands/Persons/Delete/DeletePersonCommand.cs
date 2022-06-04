using MediatR;
using Shared.Application.Mediatr;

namespace TBC.Persons.Application.Commands.Persons.Delete
{
    public class DeletePersonCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }
}
