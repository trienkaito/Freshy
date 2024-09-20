using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;

public sealed class Shipping : AggregateRoot<ShippingId>
{
    private readonly List<OrderDetail> _orderDetails = new();

    private Shipping(
        ShippingId id,
        string name,
        string description,
        string featureImage,
        double shippingPrice,
        string address,
        DateTime joinedDate) : base(id)
    {
        Name = name;
        Description = description;
        FeatureImage = featureImage;
        ShippingPrice = shippingPrice;
        Address = address;
        JoinedDate = joinedDate;
    }

    #region Properties

    public string Name { get; private set; }
    public string FeatureImage { get; private set; }
    public string Description { get; private set; }
    public double ShippingPrice { get; private set; }
    public string Address { get; private set; }
    public DateTime JoinedDate { get; private set; }
    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    #endregion Properties

    #region Functions

    public static Shipping Create(
        string name,
        string description,
        string featureImage,
        double shippingPrice,
        string address)
    {
        return new Shipping(
            ShippingId.CreateUnique(),
            name,
            description,
            featureImage,
            shippingPrice,
            address,
            DateTime.Today
        );
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Shipping()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}