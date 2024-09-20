using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Events;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.CreateReview;

public record CreateReviewCommand
(
    Guid CustomerId,
    Guid ProductId,
    string Content,
    int Rating
) : ICommand<CommandResult>;

public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, CommandResult>
{
    private readonly IReviewRepository _reviewRepository;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<CommandResult> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _reviewRepository.UnitOfWork.BeginTransaction();

            var review = Review.CreateNewReview(
                ProductId.Create(request.ProductId),
                CustomerId.Create(value: request.CustomerId),
                request.Content,
                request.Rating
            );

            var @event = new CustomerCreatedReview(
                review.Id.Value,
                request.CustomerId
            );

            review.AddDomainEvent(@event);

            await _reviewRepository.InsertAsync(review);
            await _reviewRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}