using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.Shared.Commands
{
    public class VoucherCodeCommandValidation : AbstractValidator<VoucherCodeCommand>
    {
        public VoucherCodeCommandValidation() 
        {
            RuleFor(c => c.Value)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1);
        }
    }
}
