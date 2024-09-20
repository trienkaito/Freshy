using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    private const string SUPPLIER_TABLE = "Suppliers";
    private const string SCHEMA = "supplier";

    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        ConfigSupplier(builder);
    }

    private static void ConfigSupplier(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable(SUPPLIER_TABLE, SCHEMA);

        builder.HasKey(supplier => supplier.Id);
        builder.Property(supplier => supplier.Id)
            .HasConversion(id => id.Value, value => SupplierId.Create(value))
            .ValueGeneratedNever();

        builder.Property(supplier => supplier.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        builder.HasIndex(supplier => supplier.Name)
            .IsUnique();

        builder.Property(supplier => supplier.Description)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(supplier => supplier.IsValid)
            .HasColumnType("BIT");

        builder.Property(supplier => supplier.JoinedDate)
            .HasColumnType("DATETIME");

        builder.Property(supplier => supplier.FeatureImage)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);
    }
}