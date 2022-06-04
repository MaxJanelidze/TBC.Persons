using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Domain.Aggregates.Persons;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : ICommandHandler<AddRelatedPersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;

        public AddRelatedPersonCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var masterPerson = await TryGetPerson(request.MastertPersonId);
            var relatedPerson = await TryGetPerson(request.RelatedPersonId);

            if (masterPerson.RelatedPersons.Any(x => x.RelatedPersonId == request.RelatedPersonId))
            {
                return Unit.Value;
            }

            masterPerson.AddRelatedPerson(relatedPerson, request.RelationType);

            _unitOfWork.PersonRepository.Update(masterPerson);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Person> TryGetPerson(int id)
        {
            var person = await _unitOfWork.PersonRepository.OfIdAsync(id);

            if (person == null)
            {
                throw new NotFoundException(_localizer["PersonNotFound"]);
            }

            return person;
        }
    }
}
