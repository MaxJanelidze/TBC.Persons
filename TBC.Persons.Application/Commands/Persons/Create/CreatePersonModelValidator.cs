using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.Create
{
    public class CreatePersonModelValidator : AbstractValidator<CreatePersonModel>
    {
        public CreatePersonModelValidator(IStringLocalizer<ResourceStrings> localizer)
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage(localizer["RequiredField"].Value);
            RuleFor(x => x.Firstname).MinimumLength(2).WithMessage(string.Format(localizer["HasMinLengthOf"].Value, 2));
            RuleFor(x => x.Firstname).MaximumLength(50).WithMessage(string.Format(localizer["HasMaxLengthOf"].Value, 50));

            RuleFor(x => x.Lastname).NotEmpty().WithMessage(localizer["RequiredField"].Value);
            RuleFor(x => x.Lastname).MinimumLength(2).WithMessage(string.Format(localizer["HasMinLengthOf"].Value, 2));
            RuleFor(x => x.Lastname).MaximumLength(50).WithMessage(string.Format(localizer["HasMaxLengthOf"].Value, 50));

            RuleFor(x => x.PersonalNumber).NotEmpty().WithMessage(localizer["RequiredField"].Value);
            RuleFor(x => x.PersonalNumber).Length(11).WithMessage(string.Format(localizer["HasFixedLengthOf"].Value, 11));
            RuleFor(x => x.PersonalNumber).Matches("^[0-9]{11}$").WithMessage(localizer["OnlyDigits"].Value);

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage(localizer["RequiredField"].Value);
            RuleFor(x => x.BirthDate).LessThan(DateTime.Now.Date.AddYears(-18)).WithMessage(string.Format(localizer["HasMinAgeRestrictionOf"].Value, 18));
        }
    }
}
