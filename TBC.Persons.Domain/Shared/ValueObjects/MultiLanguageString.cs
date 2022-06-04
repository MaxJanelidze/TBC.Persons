using Shared.Domain;
using System;
using TBC.Persons.Shared;

namespace TBC.Persons.Domain.Shared.ValueObjects
{
    public class MultiLanguageString : ValueObject<MultiLanguageString>
    {
        protected MultiLanguageString()
        {
        }

        public MultiLanguageString(string georgian, string english)
            : this()
        {
            Georgian = georgian;
            English = english;
        }

        public string Georgian { get; private set; }

        public string English { get; private set; }

        public string Translate(Language language) => language switch
        {
            Language.Georgian => Georgian,
            Language.English => English,
            _ => throw new Exception("Not Supported Language")
        };

        public override string ToString()
        {
            return string.Format(@"{0}_{1}", Georgian, English);
        }
    }
}
