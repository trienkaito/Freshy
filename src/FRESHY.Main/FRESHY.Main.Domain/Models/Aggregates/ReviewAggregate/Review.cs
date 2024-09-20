using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;

public sealed class Review : AggregateRoot<ReviewId>
{
    public Review(
        ReviewId id,
        ProductId productId,
        CustomerId? customerId,
        EmployeeId? employeeId,
        string content,
        DateTime createdDate,
        int? ratingValue,
        ReviewId? parentReviewId,
        int likeCount,
        bool isBeingReply) : base(id)
    {
        ProductId = productId;
        Content = content;
        CreatedDate = createdDate;
        RatingValue = ratingValue;
        ParentReviewId = parentReviewId;
        LikeCount = likeCount;
        IsBeingReply = isBeingReply;
        CustomerId = customerId;
        EmployeeId = employeeId;
    }

    #region Properties

    public Product Product { get; private set; } = null!;
    public ProductId ProductId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public CustomerId? CustomerId { get; private set; }
    public Employee Employee { get; private set; } = null!;
    public EmployeeId? EmployeeId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public ReviewId? ParentReviewId { get; private set; } // A ReviewId being replied.
    public int? RatingValue { get; private set; }
    public int LikeCount { get; private set; } //Auto increase when a Like being created
    public bool IsBeingReply { get; private set; }

    #endregion Properties

    #region Functions

    public static Review CreateNewReview(
        ProductId productId,
        CustomerId customerId,
        string content,
        int ratingValue)
    {
        return new Review(
            ReviewId.CreateUnique(),
            productId,
            customerId,
            null,
            content,
            DateTime.UtcNow,
            ratingValue,
            null,
            0,
            false);
    }

    public static Review ReplyCustomerReview(
        ProductId productId,
        EmployeeId employeeId,
        string content,
        ReviewId subRepliedReviewId
    )
    {
        return new Review(
            ReviewId.CreateUnique(),
            productId,
            null,
            employeeId,
            content,
            DateTime.UtcNow,
            null,
            subRepliedReviewId,
            0,
            false);
    }

    public static Review ReviewBeingReply(Review review)
    {
        review.IsBeingReply = true;
        return review;
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Review()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}