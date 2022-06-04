using Shared.Application.Extensions;
using Shared.Application.Mediatr;
using Shared.Application.Pagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Domain;

namespace TBC.Persons.Application.Queries.GetPersons
{
    public class GetPersonsQueryHandler : IQueryHandler<GetPersonsQuery, PagedList<PersonModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public GetPersonsQueryHandler(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            _unitOfWork = unitOfWork;
            _applicationContext = applicationContext;
        }

        public async Task<PagedList<PersonModel>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.PersonRepository
                .Query(x => !x.IsDeleted)
                .AndIf(request.Firstname, x => x.Firstname.Georgian == request.Firstname || x.Firstname.English == request.Firstname)
                .AndIf(request.Lastname, x => x.Lastname.Georgian == request.Lastname || x.Lastname.English == request.Lastname)
                .AndIf(request.PersonalNumber, x => x.PersonalNumber == request.PersonalNumber)
                .AndIf(request.Gender, x => x.Gender == request.Gender)
                .AndIf(request.CityId, x => x.CityId == request.CityId)
                .AndIf(request.Search, x =>
                    x.Firstname.Georgian == request.Search || x.Firstname.English == request.Search ||
                    x.Lastname.Georgian == request.Search || x.Lastname.English == request.Search ||
                    x.PersonalNumber == request.Search);

            var totalCount = query.Count();
            var persons = query
                .Select(x => new PersonModel
                {
                    Id = x.Id,
                    Firstname = x.Firstname.Translate(_applicationContext.Language),
                    Lastname = x.Lastname.Translate(_applicationContext.Language),
                    PersonalNumber = x.PersonalNumber
                })
                .SortAndPage(request)
                .ToList();

            return new PagedList<PersonModel>(persons, totalCount, request.PageSize, request.CurrentPage);
        }
    }
}
