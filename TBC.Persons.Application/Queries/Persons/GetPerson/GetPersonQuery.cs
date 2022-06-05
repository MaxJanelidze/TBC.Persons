using Shared.Application.Mediatr;

namespace TBC.Persons.Application.Queries.Persons.GetPerson
{
    public class GetPersonQuery : IQuery<PersonModel>
    {
        public int Id { get; set; }
    }
}
