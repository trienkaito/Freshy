using FluentValidation;
using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.UpdateProductType;

namespace FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.CreateProductType
{
    public class UpdateProductTypeCommandValidation : AbstractValidator<UpdateProductTypeCommand>
    {
        public UpdateProductTypeCommandValidation()
        {
            RuleFor(x => x.TypeId)
                .NotEmpty()
                .NotNull();
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
