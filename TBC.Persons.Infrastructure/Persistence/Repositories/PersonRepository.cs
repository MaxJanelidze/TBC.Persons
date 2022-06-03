using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using System.Threading.Tasks;
using TBC.Persons.Domain.Aggregates.Persons;
using TBC.Persons.Domain.Aggregates.Persons.Repositories;
using TBC.Persons.Infrastructure.Persistence.Context;

namespace TBC.Persons.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : BaseRepository<PersonDbContext, Person, int>, IPersonRepository
    {
        public PersonRepository(PersonDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsByPersonalNumber(string personalNumber)
        {
            return await _context.Set<Person>().AnyAsync(x => x.PersonalNumber == personalNumber);
        }
    }
}
