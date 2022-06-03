using Shared.Infrastructure;
using TBC.Persons.Domain.Aggregates.Cities;
using TBC.Persons.Domain.Aggregates.Cities.Repositories;
using TBC.Persons.Infrastructure.Persistence.Context;

namespace TBC.Persons.Infrastructure.Persistence.Repositories
{
    public class CityRepository : BaseRepository<PersonDbContext, City, int>, ICityRepository
    {
        public CityRepository(PersonDbContext context)
            : base(context)
        {
        }
    }
}
