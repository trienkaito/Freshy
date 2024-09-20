using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.ReplyReview
{
    public class ReplyReviewCommandValidation : AbstractValidator<ReplyReviewCommand>
    {
        public ReplyReviewCommandValidation() 
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty();

            RuleFor(x => x.ProductId)
                .NotEmpty();


            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.ParentReviewId)
                .NotEmpty();
        }
    }
}
