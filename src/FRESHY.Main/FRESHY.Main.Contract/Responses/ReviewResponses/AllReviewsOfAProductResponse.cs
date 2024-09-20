namespace FRESHY.Main.Contract.Responses.ReviewResponses;

public record AllReviewsOfAProductResponse
(
    string Content,
    DateTime CreatedDate,
    Guid? ParentReviewId,
    int? RatingValue,
    int LikeCount,
    bool IsBeingReply
);