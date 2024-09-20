using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;

public class VoucherCode : ValueObject
{
    private static readonly Random random = new();
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string Value { get; set; }

    public VoucherCode(string value)
    {
        Value = value;
    }

    public static VoucherCode CreateRandom()
    {
        return new VoucherCode(new string(Enumerable.Repeat(chars, 9)
            .Select(voucher => voucher[random.Next(voucher.Length)]).ToArray()));
    }

    public static VoucherCode Create(string code)
    {
        return new VoucherCode(code);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}