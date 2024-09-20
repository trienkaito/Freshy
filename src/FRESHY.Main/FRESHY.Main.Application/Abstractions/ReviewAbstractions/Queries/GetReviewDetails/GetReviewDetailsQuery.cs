using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetReviewDetails.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetReplyOfAReview;

public record GetReviewDetailsQuery
(
    Guid ReviewId
) : IQuery<QueryResult<ReviewDetailsResult>>;

public class GetReviewDetailsQueryHandler : IQueryHandler<GetReviewDetailsQuery, QueryResult<ReviewDetailsResult>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewDetailsQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<QueryResult<ReviewDetailsResult>> Handle(GetReviewDetailsQuery request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(ReviewId.Create(request.ReviewId), review => new
        {
            review.Content,
            review.CreatedDate,
            review.RatingValue,
            review.LikeCount
        });

        var data = new ReviewDetailsResult(
            review!.Content,
            review.CreatedDate,
            review.RatingValue,
            review.LikeCount
        );

        return new QueryResult<ReviewDetailsResult>(data);
    }
}