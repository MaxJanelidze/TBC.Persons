using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Shared;

namespace TBC.Persons.Application
{
    public static class Module
    {
        public static void AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ApplicationContext, ApplicationContext>(sp =>
            {
                var language = CultureInfo.CurrentCulture.Name == "ka-GE"
                    ? Language.Georgian
                    : Language.English;

                return new ApplicationContext(language);
            });
        }
    }
}
