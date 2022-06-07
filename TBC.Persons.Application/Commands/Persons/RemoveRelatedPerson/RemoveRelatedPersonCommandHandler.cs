using MediatR;
using Shared.Application.Mediatr;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;

namespace TBC.Persons.Application.Commands.Persons.RemoveRelatedPerson
{
    public class RemoveRelatedPersonCommandHandler : ICommandHandler<RemoveRelatedPersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRelatedPersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var masterPerson = await _unitOfWork.PersonRepository.TryGetPerson(request.MastertPersonId);
            var relatedPerson = await _unitOfWork.PersonRepository.TryGetPerson(request.RelatedPersonId);

            masterPerson.RemoveRelatedPerson(relatedPerson);

            _unitOfWork.PersonRepository.Update(masterPerson);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
