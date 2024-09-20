namespace FRESHY.Main.Contract.Requests.ReviewRequests;

public record ReplyReviewRequest
(
    Guid EmployeeId,
    Guid ProductId,
    string Content,
    Guid ParentReviewId
);