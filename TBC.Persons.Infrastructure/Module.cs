using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TBC.Persons.Infrastructure.Persistence.Context;

namespace TBC.Persons.Infrastructure
{
    public static class Module
    {
        public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PersonsDatabase");

            services.AddDbContext<PersonDbContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies().EnableSensitiveDataLogging());
        }
    }
}
