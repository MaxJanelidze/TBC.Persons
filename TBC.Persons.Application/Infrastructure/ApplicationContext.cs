using TBC.Persons.Shared;

namespace TBC.Persons.Application.Infrastructure
{
    public class ApplicationContext
    {
        public ApplicationContext(Language language)
        {
            Language = language;
        }

        public Language Language { get; }
    }
}
