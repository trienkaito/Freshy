using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.Shared.Results
{
    public class VoucherCodeResultValidation : AbstractValidator<VoucherCodeResult>
    {
        public VoucherCodeResultValidation() 
        {
            RuleFor(c => c.Value)
               .NotNull()
               .NotEmpty()
               .MinimumLength(1);
        }
    }
}
