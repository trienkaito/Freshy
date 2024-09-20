using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate;

public sealed class OrderAddress : AggregateRoot<OrderAddressId>
{
    private OrderAddress(
        OrderAddressId id,
        CustomerId customerId,
        string name,
        string phoneNumber,
        string country,
        string city,
        string district,
        string ward,
        string details,
        bool isShippingAddress,
        bool isDefaultAddress) : base(id)
    {
        CustomerId = customerId;
        Name = name;
        PhoneNumber = phoneNumber;
        Country = country;
        City = city;
        District = district;
        Ward = ward;
        Details = details;
        IsShippingAddress = isShippingAddress;
        IsDefaultAddress = isDefaultAddress;
    }

    #region Properties

    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string District { get; private set; }
    public string Ward { get; private set; }
    public string Details { get; private set; }
    public bool IsShippingAddress { get; private set; } //* Show shipping addresses for customer choose
    public bool IsDefaultAddress { get; private set; } //* If default => Consider showing for chosing shipping address
    public Customer Customer { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; }

    #endregion Properties

    #region Functions

    public static OrderAddress Create(
        CustomerId customerId,
        string name,
        string phoneNumber,
        string country,
        string city,
        string district,
        string ward,
        string details,
        bool isShippingAddress,
        bool isDefaultAddress)
    {
        return new OrderAddress(
            OrderAddressId.CreateUnique(),
            customerId,
            name,
            phoneNumber,
            country,
            city,
            district,
            ward,
            details,
            isShippingAddress,
            isDefaultAddress);
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private OrderAddress()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}