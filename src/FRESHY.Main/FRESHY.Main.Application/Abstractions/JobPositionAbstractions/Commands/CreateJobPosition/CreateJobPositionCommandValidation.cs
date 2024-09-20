using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Commands.CreateJobPosition
{
    public class CreateJobPositionCommandValidation : AbstractValidator<CreateJobPositionCommand>
    {
        public CreateJobPositionCommandValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .Must(name => !ContainsSpecialCharactersOrDigits(name))
            .MinimumLength(2);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(255);
            
            RuleFor(x => x.Salary)
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
