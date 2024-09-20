using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;

public sealed class ProductLike : AggregateRoot<ProductLikeId>
{
    public ProductLike(
        ProductLikeId id,
        ProductId productId,
        CustomerId customerId) : base(id)
    {
        ProductId = productId;
        CustomerId = customerId;
    }

    #region Properties

    public Product Product { get; private set; } = null!;
    public ProductId ProductId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; }

    #endregion Properties

    #region Fucntions

    public static ProductLike Create(
        ProductId productId,
        CustomerId customerId)
    {
        return new ProductLike(
            ProductLikeId.CreateUnique(),
            productId,
            customerId);
    }

    #endregion Fucntions
}