using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Domain;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Queries.Persons.GetPerson
{
    public class GetPersonQueryHandler : IQueryHandler<GetPersonQuery, PersonModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;
        private readonly ApplicationContext _applicationContext;

        public GetPersonQueryHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer, ApplicationContext applicationContext)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _applicationContext = applicationContext;
        }

        public async Task<PersonModel> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.PersonRepository.OfIdAsync(request.Id);

            if (person == null)
            {
                if (person == null)
                {
                    throw new NotFoundException(_localizer["PersonNotFound"]);
                }
            }

            return new PersonModel
            {
                Id = person.Id,
                Firstname = person.Firstname.Translate(_applicationContext.Language),
                Lastname = person.Lastname.Translate(_applicationContext.Language),
                Gender = person.Gender,
                PersonalNumber = person.PersonalNumber,
                BirthDate = person.BirthDate,
                CityId = person.CityId,
                PictureFileAddress = person.PictureFileAddress,
                Phones = person.Phones.Select(x => new Shared.Models.Phone { Number = x.Number, PhoneType = x.PhoneType }),
                RelatedPersons = person.RelatedPersons.Select(x => new Shared.Models.RelatedPerson
                {
                    Id = x.RelatedPersonId,
                    Firstname = x.RelatedPerson.Firstname.Translate(_applicationContext.Language),
                    Lastname = x.RelatedPerson.Lastname.Translate(_applicationContext.Language),
                    RelationType = x.RelationType
                })
            };
        }
    }
}
