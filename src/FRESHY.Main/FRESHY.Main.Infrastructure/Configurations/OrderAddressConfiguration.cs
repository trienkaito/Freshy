using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class OrderAddressConfiguration : IEntityTypeConfiguration<OrderAddress>
{
    private const string ORDER_ADDRESS_TABLE = "OrderAddresses";
    private const string SCHEMA = "customer";

    public void Configure(EntityTypeBuilder<OrderAddress> builder)
    {
        ConfigOrderAddressTable(builder);
        AddCustomerForeignKey(builder);
    }

    private static void ConfigOrderAddressTable(EntityTypeBuilder<OrderAddress> builder)
    {
        builder.ToTable(ORDER_ADDRESS_TABLE, SCHEMA);

        builder.HasKey(address => address.Id);

        builder.Property(address => address.Id)
               .HasConversion(id => id.Value, value => OrderAddressId.Create(value))
               .ValueGeneratedNever();

        builder.Property(address => address.Name)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 255);

        builder.Property(address => address.PhoneNumber)
            .HasColumnType(typeName: "CHAR")
            .HasMaxLength(maxLength: 13);

        builder.Property(address => address.Country)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 255);

        builder.Property(address => address.City)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 255);

        builder.Property(address => address.District)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 255);

        builder.Property(address => address.Ward)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 255);

        builder.Property(address => address.Details)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 255);

        builder.Property(address => address.IsDefaultAddress)
            .HasColumnType("BIT");

        builder.Property(address => address.IsShippingAddress)
            .HasColumnType("BIT");
    }

    private static void AddCustomerForeignKey(EntityTypeBuilder<OrderAddress> builder)
    {
        builder.HasOne(address => address.Customer)
           .WithMany(customer => customer.OrderAddresses)
           .HasForeignKey(address => address.CustomerId);
    }
}