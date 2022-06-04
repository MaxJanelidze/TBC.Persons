using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Shared;

namespace TBC.Persons.Application
{
    public static class Module
    {
        public static void AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining(typeof(Module));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
