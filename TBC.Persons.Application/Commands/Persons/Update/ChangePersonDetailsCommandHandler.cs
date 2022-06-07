using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Domain.Shared.ValueObjects;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.Update
{
    public class ChangePersonDetailsCommandHandler : ICommandHandler<ChangePersonDetailsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;

        public ChangePersonDetailsCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(ChangePersonDetailsCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.PersonRepository.TryGetPerson(request.Id);

            if (_unitOfWork.PersonRepository.Query(x => x.PersonalNumber == request.PersonalNumber && x.Id != request.Id).Any())
            {
                throw new ClientErrorException(_localizer["PersonAlreadyExists"].Value);
            }

            var city = await _unitOfWork.CityRepository.OfIdAsync(request.CityId);

            if (city == null)
            {
                throw new ClientErrorException(_localizer["CityNotFound"].Value);
            }

            person
                .AssignName(new MultiLanguageString(request.Firstname, null), new MultiLanguageString(request.Lastname, null))
                .AssignPersonalInformation(request.Gender, request.BirthDate, request.PersonalNumber)
                .AssignContactInformation(request.Phones.Select(x => x.ToDomainObject()).ToArray())
                .AssignCity(city);

            _unitOfWork.PersonRepository.Update(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
