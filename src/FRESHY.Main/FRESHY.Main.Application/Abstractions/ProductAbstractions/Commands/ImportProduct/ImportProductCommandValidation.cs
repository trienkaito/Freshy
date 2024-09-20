using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.ImportProduct
{
    public class ImportProductCommandValidation : AbstractValidator<ImportProductCommand>
    {
        public ImportProductCommandValidation()
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
                .MinimumLength(10)
                .MaximumLength(255);
            RuleFor(x => x.TypeId)
                .NotEmpty();
   
            RuleFor(x => x.SupplierId)
                .NotEmpty();

            RuleFor(x => x.Dom)
                .NotEmpty()
                .Must(BeAValidDate);

            RuleFor(x => x.ExpiryDate)
                .NotEmpty()
                .Must(BeAValidDate);
            RuleFor(x => x.IsShowToCustomer)
                .NotEmpty();
            RuleFor(x => x.EmployeeId)
                .NotEmpty();
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
        private bool BeAValidDate(string date)
        {
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                return false;
            return dob < DateTime.Today;
        }
        private bool BeAPrice(double? value)
        {
            return double.TryParse(value.ToString(), out double result) && result > 0;
        }
    }
}
