using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Domain.Aggregates.Persons;
using TBC.Persons.Domain.Shared.ValueObjects;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.Create
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.PersonRepository.ExistsByPersonalNumber(request.PersonalNumber))
            {
                throw new ClientErrorException(_localizer["PersonAlreadyExists"].Value);
            }

            var city = await _unitOfWork.CityRepository.OfIdAsync(request.CityId);

            if (city == null)
            {
                throw new ClientErrorException(_localizer["CityNotFound"].Value);
            }

            var person = new Person(new MultiLanguageString(request.Firstname, null), new MultiLanguageString(request.Lastname, null))
                .AddPersonalInformation(request.Gender, request.BirthDate, request.PersonalNumber)
                .AddContactInformation(request.Phones.Select(x => x.ToDomainObject()).ToArray())
                .AssignCity(city);

            await _unitOfWork.PersonRepository.InsertAsync(person, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
