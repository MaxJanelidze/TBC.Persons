using Shared.Domain;
using System.Threading.Tasks;

namespace TBC.Persons.Domain.Aggregates.Persons.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {
        Task<bool> ExistsByPersonalNumber(string personalNumber);

        Task<Person> TryGetPerson(int id);
    }
}
