using AutoMapper;
using TBC.Persons.Application.Commands.Persons.Create;
using TBC.Persons.Application.Commands.Persons.Update;
using TBC.Persons.Application.Queries.Persons.GetPersons;

namespace TBC.Persons.Application.MappingProfiles
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            CreateMap<CreatePersonModel, CreatePersonCommand>();
            CreateMap<ChangePersonDetailsModel, ChangePersonDetailsCommand>();
            CreateMap<GetPersonsModel, GetPersonsQuery>();
        }
    }
}
