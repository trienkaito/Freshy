using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;

namespace FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId>
{
    private readonly List<OrderAddress>? _orderAddresses = new();
    private readonly List<OrderDetail> _orderDetails = new();
    private readonly List<ProductLike> _likes = new();
    private readonly List<Review> _reviews = new();

    private const string DEFAULT_AVATAR = "https://res.cloudinary.com/dpvftjlm9/image/upload/v1710317404/bh6shxzutbugwiyliwc1.jpg";

    private Customer(
        CustomerId id,
        string name,
        string avatar,
        Guid accountId,
        string? email,
        string? phone,
        DateTime createdDate) : base(id)
    {
        Name = name;
        AccountId = accountId;
        Avatar = avatar;
        CreatedDate = createdDate;
        Email = email;
        Phone = phone;
    }

    #region Properties

    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string Name { get; private set; }
    public string Avatar { get; private set; }
    public Guid AccountId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public Cart Cart { get; private set; } = null!;
    public IReadOnlyList<OrderAddress>? OrderAddresses => _orderAddresses?.AsReadOnly();
    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();
    public IReadOnlyList<ProductLike> Likes => _likes.AsReadOnly();
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    #endregion Properties

    #region Functions

    public static Customer Create(
        string name,
        Guid accountId,
        string? email,
        string? phone)
    {
        return new Customer(
            CustomerId.CreateUnique(),
            name,
            DEFAULT_AVATAR,
            accountId,
            email,
            phone,
            DateTime.Today);
    }

    public void UpdateCustomerInfos(
        string name,
        string avatar
    )
    {
        Name = name;
        Avatar = avatar;
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Customer()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}