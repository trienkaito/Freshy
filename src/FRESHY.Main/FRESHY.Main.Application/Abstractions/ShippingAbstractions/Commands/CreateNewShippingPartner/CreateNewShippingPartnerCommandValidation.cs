using FluentValidation;

namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.CreateNewShippingPartner
{
    public class CreateNewShippingPartnerCommandValidation : AbstractValidator<CreateNewShippingPartnerCommand>
    {
        public CreateNewShippingPartnerCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .Must(name => !ContainsSpecialCharactersOrDigits(name));

            RuleFor(x => x.FeatureImage)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1);
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(255);
            RuleFor(x => x.ShippingPrice)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Address)
                .NotEmpty()
                .MinimumLength(1);
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
