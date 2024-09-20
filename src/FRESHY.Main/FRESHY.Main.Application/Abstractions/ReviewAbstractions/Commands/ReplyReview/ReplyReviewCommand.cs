using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Events;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.ReplyReview;

public record ReplyReviewCommand
(
    Guid EmployeeId,
    Guid ProductId,
    string Content,
    Guid ParentReviewId
) : ICommand<CommandResult>;

public class ReplyReviewCommandHandler : ICommandHandler<ReplyReviewCommand, CommandResult>
{
    private readonly IReviewRepository _reviewRepository;

    public ReplyReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<CommandResult> Handle(ReplyReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _reviewRepository.UnitOfWork.BeginTransaction();

            var parentReview = await _reviewRepository.GetByIdAsync(ReviewId.Create(request.ParentReviewId));

            var reply = Review.ReplyCustomerReview(
                ProductId.Create(request.ProductId),
                EmployeeId.Create(request.EmployeeId),
                request.Content,
                ReviewId.Create(request.ParentReviewId)
            );

            var @event = new EmployeeRepliedReview(
                reply.Id.Value,
                request.EmployeeId,
                reply.ParentReviewId!.Value);

            parentReview = Review.ReviewBeingReply(parentReview!);
            reply.AddDomainEvent(@event);

            _reviewRepository.Update(parentReview);
            await _reviewRepository.InsertAsync(reply);
            await _reviewRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}