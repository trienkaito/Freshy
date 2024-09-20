using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
{
    private const string SHIPPING_TABLE = "ShippingCompanies";
    private const string SCHEMA = "shipping";

    public void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.ToTable(SHIPPING_TABLE, SCHEMA);

        builder.HasKey(shipping => shipping.Id);

        builder.Property(shipping => shipping.Id)
            .HasConversion(id => id.Value, value => ShippingId.Create(value))
            .ValueGeneratedNever();

        builder.Property(shipping => shipping.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(shipping => shipping.Description)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(shipping => shipping.Address)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(shipping => shipping.JoinedDate)
            .HasColumnType("DATETIME");

        builder.Property(shipping => shipping.ShippingPrice)
                .HasColumnName("ShippingPrice")
                .HasColumnType("MONEY");

        builder.Property(shipping => shipping.FeatureImage)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);
    }
}