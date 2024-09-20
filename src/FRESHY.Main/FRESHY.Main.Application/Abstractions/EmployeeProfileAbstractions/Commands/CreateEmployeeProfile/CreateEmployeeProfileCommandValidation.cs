using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.CreateEmployeeProfile
{
    public class CreateEmployeeProfileCommandValidation : AbstractValidator<CreateEmployeeProfileCommand>
    {
        public CreateEmployeeProfileCommandValidation() 
        {
            RuleFor(c => c.AccountId)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.Fullname)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .Must(name => !ContainsSpecialCharactersOrDigits(name));
            RuleFor(c => c.Avatar)
               .NotEmpty()
               .NotNull()
               .MinimumLength(1);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]*$")
                .Length(10);
            RuleFor(x => x.Ssn)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]*$")
                .MaximumLength(12)
                .MinimumLength(8);
            RuleFor(x => x.Dob)
                .NotEmpty()
                .Must(BeAValidDate);
            RuleFor(x => x.LivingAddress)
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(x => x.Hometown)
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(x => x.CvLink)
                .NotEmpty()
                .MinimumLength(1);
            RuleFor(x => x.JobPositionId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .NotNull();
        }

        private bool BeAValidDate(string date)
        {
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                return false;
            DateTime minDate = new DateTime(1900, 1, 1);
            DateTime maxDate = DateTime.Now.Date;
            return dob >= minDate && dob <= maxDate;
        }
        private bool ContainsSpecialCharactersOrDigits(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
