using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.AddItemToCartCommand
{
    public class AddItemToCartCommandValidation : AbstractValidator<AddItemToCartCommand>
    {
        public AddItemToCartCommandValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.ProductId)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.ProductUnitId)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.BoughtQuantity)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1);
        }
    }

}
