using FluentValidation;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandValidation : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidation() 
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .MinimumLength(2)
               .Must(name => !ContainsSpecialCharactersOrDigits(name));
            RuleFor(x => x.FeatureImage)
                .NotEmpty()
                .MinimumLength(1);
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(255);
            RuleFor(x => x.IsValid)
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
