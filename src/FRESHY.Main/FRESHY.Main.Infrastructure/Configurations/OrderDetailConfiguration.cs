using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    private const string ORDER_DETAIL_TABLE = "OrderDetails";
    private const string SCHEMA = "order";

    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        ConfigOrderDetail(builder);
        AddCustomerForeignKey(builder);
        AddShippingForeignKey(builder);
        AddVoucherForeignKey(builder);
    }

    private static void AddVoucherForeignKey(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasOne(order => order.Voucher)
            .WithMany(voucher => voucher.OrderDetails)
            .HasForeignKey(order => order.VoucherId);
    }

    private static void AddShippingForeignKey(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasOne(order => order.Shipping)
            .WithMany(shipping => shipping.OrderDetails)
            .HasForeignKey(order => order.ShippingId);
    }

    private static void AddCustomerForeignKey(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasOne(order => order.Customer)
            .WithMany(customer => customer.OrderDetails)
            .HasForeignKey(order => order.CustomerId);
    }

    private static void ConfigOrderDetail(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(ORDER_DETAIL_TABLE, SCHEMA);

        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
            .HasConversion(id => id.Value, value => OrderDetailId.Create(value))
            .ValueGeneratedNever();

        builder.Property(order => order.OrderAddress)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(order => order.CreatedDate)
            .HasColumnType("DATETIME");

        builder.Property(order => order.OrderStatus)
            .HasConversion<string>()
            .HasColumnType("VARCHAR")
            .HasMaxLength(15)
            .HasDefaultValue(OrderStatus.SUCCESSED);

        builder.Property(order => order.PaymentType)
            .HasConversion<string>()
            .HasColumnType("VARCHAR")
            .HasMaxLength(15);
    }
}