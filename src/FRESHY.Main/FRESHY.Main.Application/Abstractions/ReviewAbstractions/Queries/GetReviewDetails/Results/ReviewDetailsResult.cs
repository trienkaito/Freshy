namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetReviewDetails.Results;

public record ReviewDetailsResult
(
    string Content,
    DateTime CreatedDate,
    int? RatingValue,
    int LikeCount
);