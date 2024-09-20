namespace FRESHY.Main.Contract.Requests.ReviewRequests;

public record CreateReviewRequest
(
    Guid GoodId,
    Guid ReviewerId,
    string Content,
    int Rating
);