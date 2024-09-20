using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.CreateReview
{
    public class CreateReviewCommandValidation : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidation() 
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5);
        }
    }
}
