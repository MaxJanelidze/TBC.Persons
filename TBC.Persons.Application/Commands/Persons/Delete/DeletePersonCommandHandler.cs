using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.Delete
{
    public class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.PersonRepository.TryGetPerson(request.Id);

            _unitOfWork.PersonRepository.Delete(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
