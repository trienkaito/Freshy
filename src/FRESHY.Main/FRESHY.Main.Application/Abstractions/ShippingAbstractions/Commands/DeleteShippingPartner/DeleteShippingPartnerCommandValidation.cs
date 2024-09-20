using FluentValidation;

namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.DeleteShippingPartner
{
    public class DeleteShippingPartnerCommandValidation : AbstractValidator<DeleteShippingPartnerCommand>
    {
        public DeleteShippingPartnerCommandValidation()
        {
            RuleFor(c => c.ShippingId)
                .NotEmpty();
        }
    }
}

