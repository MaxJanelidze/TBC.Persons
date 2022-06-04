using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Domain.Aggregates.Persons;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.RemoveRelatedPerson
{
    public class RemoveRelatedPersonCommandHandler : ICommandHandler<RemoveRelatedPersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;

        public RemoveRelatedPersonCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(RemoveRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var masterPerson = await TryGetPerson(request.MastertPersonId);
            var relatedPerson = await TryGetPerson(request.RelatedPersonId);

            masterPerson.RemoveRelatedPerson(relatedPerson);

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
