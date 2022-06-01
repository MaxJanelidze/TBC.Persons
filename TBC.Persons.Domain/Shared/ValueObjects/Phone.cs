using Shared.Domain;

namespace TBC.Persons.Domain.Shared.ValueObjects
{
    public class Phone : ValueObject<Phone>
    {
        private Phone()
        {
        }

        public Phone(string number, PhoneType phoneType)
            : this()
        {
            Number = number;
            PhoneType = phoneType;
        }

        public string Number { get; private set; }

        public PhoneType PhoneType { get; private set; }
    }

    public enum PhoneType
    {
        Mobile,
        Home,
        Office
    }
}
