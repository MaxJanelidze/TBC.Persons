using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBC.Persons.Domain;
using TBC.Persons.Domain.Aggregates.Cities.Repositories;
using TBC.Persons.Domain.Aggregates.Persons.Repositories;
using TBC.Persons.Infrastructure.Persistence.Behaviours;
using TBC.Persons.Infrastructure.Persistence.Context;
using TBC.Persons.Infrastructure.Persistence.Repositories;

namespace TBC.Persons.Infrastructure
{
    public static class Module
    {
        public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            var connectionString = configuration.GetConnectionString("PersonsDatabase");

            services.AddDbContext<PersonDbContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies().EnableSensitiveDataLogging());

            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
