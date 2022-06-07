using Shared.Application.Extensions;
using Shared.Application.Mediatr;
using Shared.Application.Pagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Domain;

namespace TBC.Persons.Application.Queries.Cities.GetCities
{
    public class GetCitiesQueryHandler : IQueryHandler<GetCitiesQuery, PagedList<CitiesListItemModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public GetCitiesQueryHandler(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            _unitOfWork = unitOfWork;
            _applicationContext = applicationContext;
        }

        public async Task<PagedList<CitiesListItemModel>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.CityRepository
                .Query(x => !x.IsDeleted)
                .AndIf(request.Name, x => x.Name.Georgian == request.Name || x.Name.English == request.Name);

            var totalCount = query.Count();

            var cities = query
                .Select(x => new CitiesListItemModel
                {
                    Id = x.Id,
                    Name = x.Name.Translate(_applicationContext.Language)
                })
                .SortAndPage(request)
                .ToList();

            return new PagedList<CitiesListItemModel>(cities, totalCount, request.PageSize, request.CurrentPage);
        }
    }
}
