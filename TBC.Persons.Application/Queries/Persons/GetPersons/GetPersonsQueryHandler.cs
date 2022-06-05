using Shared.Application.Extensions;
using Shared.Application.Mediatr;
using Shared.Application.Pagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Domain;

namespace TBC.Persons.Application.Queries.Persons.GetPersons
{
    public class GetPersonsQueryHandler : IQueryHandler<GetPersonsQuery, PagedList<PersonsListItemModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public GetPersonsQueryHandler(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            _unitOfWork = unitOfWork;
            _applicationContext = applicationContext;
        }

        public async Task<PagedList<PersonsListItemModel>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.PersonRepository
                .Query(x => !x.IsDeleted)
                .AndIf(request.Firstname, x => x.Firstname.Georgian == request.Firstname || x.Firstname.English == request.Firstname)
                .AndIf(request.Lastname, x => x.Lastname.Georgian == request.Lastname || x.Lastname.English == request.Lastname)
                .AndIf(request.PersonalNumber, x => x.PersonalNumber == request.PersonalNumber)
                .AndIf(request.Gender, x => x.Gender == request.Gender)
                .AndIf(request.CityId, x => x.CityId == request.CityId)
                .AndIf(request.Search, x =>
                    x.Firstname.Georgian.ToLower().Contains(request.Search.ToLower()) || x.Firstname.English.ToLower().Contains(request.Search.ToLower()) ||
                    x.Lastname.Georgian.ToLower().Contains(request.Search.ToLower()) || x.Lastname.English.ToLower().Contains(request.Search.ToLower()) ||
                    x.PersonalNumber.ToLower().Contains(request.Search.ToLower()));

            var totalCount = query.Count();
            var persons = query
                .Select(x => new PersonsListItemModel
                {
                    Id = x.Id,
                    Firstname = x.Firstname.Translate(_applicationContext.Language),
                    Lastname = x.Lastname.Translate(_applicationContext.Language),
                    PersonalNumber = x.PersonalNumber
                })
                .SortAndPage(request)
                .ToList();

            return new PagedList<PersonsListItemModel>(persons, totalCount, request.PageSize, request.CurrentPage);
        }
    }
}
