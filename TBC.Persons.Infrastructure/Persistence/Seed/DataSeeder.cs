using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBC.Persons.Domain.Aggregates.Cities;
using TBC.Persons.Domain.Aggregates.Persons;
using TBC.Persons.Domain.Shared.ValueObjects;
using TBC.Persons.Infrastructure.Persistence.Context;

namespace TBC.Persons.Infrastructure.Persistence.Seed
{
    public class DataSeeder
    {
        public static async Task Seed(PersonDbContext context)
        {
            await CitiesSeeder.Seed(context);
            await PersonsSeeder.Seed(context);

            await context.SaveChangesAsync();
        }
    }

    public class CitiesSeeder
    {
        public static async Task Seed(PersonDbContext context)
        {
            if (!await context.Set<City>().AnyAsync())
            {
                var cities = GetCities();

                await context.Set<City>().AddRangeAsync(cities);
            }
        }

        private static IEnumerable<City> GetCities()
        {
            return new List<City>
            {
                new City(new MultiLanguageString("თბილისი", "Tbilisi")),
                new City(new MultiLanguageString("ქუთაისი", "Kutaisi")),
                new City(new MultiLanguageString("წყალტუბო", "Tskaltubo"))
            };
        }
    }

    public class PersonsSeeder
    {
        public static async Task Seed(PersonDbContext context)
        {
            if (!await context.Set<Person>().AnyAsync())
            {
                var persons = GetPersons();

                await context.Set<Person>().AddRangeAsync(persons);
            }
        }

        private static IEnumerable<Person> GetPersons()
        {
            var city = new City(new MultiLanguageString("ხაშური", "Khashuri"));
            return new List<Person>
            {
                new Person(new MultiLanguageString("გელა", "Gela"), new MultiLanguageString("მენაბდე", "Menabde")).AddPersonalInformation(Gender.Male, new DateTime(1990, 2, 4), "11111111100").AssignCity(city),
                new Person(new MultiLanguageString("გულნარა", "Gulnara"), new MultiLanguageString("ხაბაზი", "Khabazi")).AddPersonalInformation(Gender.Female, new DateTime(1993, 5, 12), "11111111000").AssignCity(city)
            };
        }
    }
}
