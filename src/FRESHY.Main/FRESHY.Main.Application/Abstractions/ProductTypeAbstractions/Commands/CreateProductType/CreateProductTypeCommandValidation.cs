using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.CreateProductType
{
    public class CreateProductTypeCommandValidation : AbstractValidator<CreateProductTypeCommand>
    {
        public CreateProductTypeCommandValidation() 
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .Must(name => !ContainsSpecialCharactersOrDigits(name));
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
