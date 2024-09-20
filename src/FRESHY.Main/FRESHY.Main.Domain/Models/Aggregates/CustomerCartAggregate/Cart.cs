using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;

public sealed class Cart : AggregateRoot<CartId>
{
    private readonly List<CartItem> _cartItems = new();

    private Cart(
        CartId id,
        CustomerId customerId) : base(id)
    {
        CustomerId = customerId;
    }

    #region Properties

    public Customer Customer { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; }
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

    #endregion Properties

    #region Functions

    public static Cart Create(
        CustomerId customerId
    )
    {
        return new Cart(
            CartId.CreateUnique(),
            customerId);
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Cart()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}