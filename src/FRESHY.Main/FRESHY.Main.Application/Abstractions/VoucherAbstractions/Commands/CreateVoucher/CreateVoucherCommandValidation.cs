using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateVoucher
{
    public class CreateVoucherCommandValidation : AbstractValidator<CreateVoucherCommand> 
    {
        public CreateVoucherCommandValidation() 
        {
            RuleFor(x => x.Code)
                .NotEmpty();

            RuleFor(x => x.StartedOn)
                .NotEmpty()
                .Must(BeValidDate);

            RuleFor(x => x.EndedOn)
                .NotEmpty()
                .Must(BeValidDate);

            RuleFor(x => x.DiscountValue)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(255);
            RuleFor(x => x.EmployeeId)
                .NotEmpty(); 
        }
        private bool BeValidDate(string date)
        {
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                return false;
            return dob<DateTime.Today;
        }
    }
}
