using FluentValidation;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.DeleteSupplier
{
    public class DeleteSupplierConmmandValidation : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierConmmandValidation()
        {
            RuleFor(c => c.SupplierId)
                .NotEmpty();
        }
    }
}
