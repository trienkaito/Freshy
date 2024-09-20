using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile
{
    public class CreateCustomerProfileCommandValidation : AbstractValidator<CreateCustomerProfileCommand>
    {
        public CreateCustomerProfileCommandValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Must(name => !ContainsSpecialCharactersOrDigits(name))
                .MinimumLength(2);
            RuleFor(c => c.AccountId)
                .NotEmpty()
                .NotNull();

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
