namespace FRESHY.Main.Contract.Responses.ReviewResponses;

public record ReviewDetailsResponse
(
    string Content,
    DateTime CreatedDate,
    int? RatingValue,
    int LikeCount
);