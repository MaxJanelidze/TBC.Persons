using TBC.Persons.Domain.Shared.ValueObjects;

namespace TBC.Persons.Application.Commands.Persons.Shared
{
    public class Phone
    {
        public string Number { get; set; }

        public PhoneType PhoneType { get; set; }

        public Domain.Shared.ValueObjects.Phone ToDomainObject()
            => new Domain.Shared.ValueObjects.Phone(Number, PhoneType);
    }
}
