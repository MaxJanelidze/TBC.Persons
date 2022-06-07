using MediatR;
using Shared.Application.Mediatr;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;

namespace TBC.Persons.Application.Commands.Persons.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : ICommandHandler<AddRelatedPersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRelatedPersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var masterPerson = await _unitOfWork.PersonRepository.TryGetPerson(request.MastertPersonId);
            var relatedPerson = await _unitOfWork.PersonRepository.TryGetPerson(request.RelatedPersonId);

            if (masterPerson.RelatedPersons.Any(x => x.RelatedPersonId == request.RelatedPersonId))
            {
                return Unit.Value;
            }

            masterPerson.AddRelatedPerson(relatedPerson, request.RelationType);

            _unitOfWork.PersonRepository.Update(masterPerson);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
