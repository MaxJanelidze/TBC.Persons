using Shared.Domain;
using TBC.Persons.Domain.Aggregates.Cities.Repositories;
using TBC.Persons.Domain.Aggregates.Persons.Repositories;

namespace TBC.Persons.Domain
{
    public interface IUnitOfWork : IGenericUnitOfWork
    {
        public IPersonRepository PersonRepository { get; set; }

        public ICityRepository CityRepository { get; set; }
    }
}
