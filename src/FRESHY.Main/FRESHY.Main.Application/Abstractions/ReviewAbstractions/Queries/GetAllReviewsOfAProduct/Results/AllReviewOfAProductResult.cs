namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetAllReviewsOfAProduct.Results;

public record AllReviewOfAProductResult
(
    string Content,
    DateTime CreatedDate,
    Guid? ParentReviewId,
    int? RatingValue,
    int LikeCount,
    bool IsBeingReply
);