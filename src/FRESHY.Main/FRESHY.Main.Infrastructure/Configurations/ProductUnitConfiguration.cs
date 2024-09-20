using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
{
    private const string PRODUCT_UNIT_TABLE = "ProductUnits";
    private const string SCHEMA = "product";

    public void Configure(EntityTypeBuilder<ProductUnit> builder)
    {
        ConfigProductUnit(builder);
        AddProductForeignKey(builder);
    }

    private static void AddProductForeignKey(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.HasOne(unit => unit.Product)
            .WithMany(product => product.Units)
            .HasForeignKey(unit => unit.ProductId);
    }

    private static void ConfigProductUnit(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.ToTable(PRODUCT_UNIT_TABLE, SCHEMA);

        builder.HasKey(unit => unit.Id);

        builder.Property(unit => unit.Id)
            .HasConversion(id => id.Value, value => ProductUnitId.Create(value))
            .ValueGeneratedNever();

        builder.Property(unit => unit.Quantity)
            .HasColumnType("INT");

        builder.Property(unit => unit.ImportPrice)
            .HasColumnType("MONEY");

        builder.Property(unit => unit.SellPrice)
            .HasColumnType("MONEY");

        builder.Property(unit => unit.UnitValue)
            .HasColumnType("FLOAT");

        builder.Property(unit => unit.UnitType)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.Property(unit => unit.UnitFeatureImage)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);
    }
}