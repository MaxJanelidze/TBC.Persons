using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Domain.Aggregates.Cities.Repositories;
using TBC.Persons.Domain.Aggregates.Persons.Repositories;
using TBC.Persons.Infrastructure.Persistence.Context;

namespace TBC.Persons.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonDbContext _context;

        public UnitOfWork(IPersonRepository personRepository, ICityRepository cityRepository, PersonDbContext context)
        {
            PersonRepository = personRepository;
            CityRepository = cityRepository;
            _context = context;
        }

        public IPersonRepository PersonRepository { get; set; }

        public ICityRepository CityRepository { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
