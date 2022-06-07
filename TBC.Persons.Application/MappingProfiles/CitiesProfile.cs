using AutoMapper;
using TBC.Persons.Application.Queries.Cities.GetCities;

namespace TBC.Persons.Application.MappingProfiles
{
    public class CitiesProfile : Profile
    {
        public CitiesProfile()
        {
            CreateMap<GetCitiesModel, GetCitiesQuery>();
        }
    }
}
