using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Shared.Common.Exceptions;
using Shared.Infrastructure;
using System.Threading.Tasks;
using TBC.Persons.Domain.Aggregates.Persons;
using TBC.Persons.Domain.Aggregates.Persons.Repositories;
using TBC.Persons.Infrastructure.Persistence.Context;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : BaseRepository<PersonDbContext, Person, int>, IPersonRepository
    {
        private readonly IStringLocalizer<ResourceStrings> _localizer;

        public PersonRepository(PersonDbContext context, IStringLocalizer<ResourceStrings> localizer)
            : base(context)
        {
            _localizer = localizer;
        }

        public async Task<bool> ExistsByPersonalNumber(string personalNumber)
        {
            return await _context.Set<Person>().AnyAsync(x => x.PersonalNumber == personalNumber);
        }

        public async Task<Person> TryGetPerson(int id)
        {
            var person = await OfIdAsync(id);

            if (person == null)
            {
                throw new NotFoundException(_localizer["PersonNotFound"]);
            }

            return person;
        }
    }
}
