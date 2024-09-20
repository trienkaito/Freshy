using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;

public sealed class Voucher : AggregateRoot<VoucherId>
{
    private readonly List<OrderDetail> _orderDetails = new();

    public Voucher(
        VoucherId id,
        VoucherCode voucherCode,
        DateTime startedOn,
        DateTime endedOn,
        float discountValue,
        string? description,
        bool isValid) : base(id)
    {
        VoucherCode = voucherCode;
        StartedOn = startedOn;
        EndedOn = endedOn;
        DiscountValue = discountValue;
        Description = description;
        IsValid = isValid;
    }

    #region Properties

    public VoucherCode VoucherCode { get; private set; }
    public DateTime StartedOn { get; private set; }
    public DateTime EndedOn { get; private set; }
    public float DiscountValue { get; private set; }
    public string? Description { get; private set; }
    public bool IsValid { get; private set; }
    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    #endregion Properties

    #region Functions

    public static Voucher Create(
        string? code,
        DateTime startedOn,
        DateTime endedOn,
        float discountValue,
        string? description,
        bool isValid = true)
    {
        return new Voucher(
            VoucherId.CreateUnique(),
            code is null ? VoucherCode.CreateRandom() : VoucherCode.Create(code),
            startedOn,
            endedOn,
            discountValue,
            description,
            isValid
        );
    }

    public static Voucher CreateDiscountForOffline(
        string code,
        float discountValue,
        string? description,
        bool isValid = true
    )
    {
        return new Voucher(
            VoucherId.CreateUnique(),
            VoucherCode.Create(code),
            DateTime.MinValue,
            DateTime.MaxValue,
            discountValue,
            description,
            isValid
        );
    }

    public void DeActivateVoucher()
    {
        IsValid = false;
    }

    public void ExtendVoucherExpiryDate(int days)
    {
        EndedOn = EndedOn.AddDays(days);
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Voucher()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}