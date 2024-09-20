using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private const string PRODUCT_TABLE = "Products";
    private const string SCHEMA = "product";

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigProduct(builder);
        AddSupplierForeignKey(builder);
        AddTypeForeignKey(builder);
    }

    private static void AddTypeForeignKey(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(product => product.Type)
            .WithMany(supplier => supplier.Products)
            .HasForeignKey(product => product.TypeId);
    }

    private static void AddSupplierForeignKey(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(product => product.Supplier)
            .WithMany(supplier => supplier.Products)
            .HasForeignKey(product => product.SupplierId);
    }

    private static void ConfigProduct(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(PRODUCT_TABLE, SCHEMA);

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .HasConversion(id => id.Value, value => ProductId.Create(value))
            .ValueGeneratedNever();

        builder.Property(product => product.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        builder.HasIndex(product => product.Name)
            .IsUnique();

        builder.Property(product => product.Description)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(product => product.CreatedDate)
            .HasColumnType("DATETIME");

        builder.Property(product => product.UpdatedDate)
           .HasColumnType("DATETIME")
           .IsRequired(false);

        builder.Property(product => product.ExpiryDate)
           .HasColumnType("DATETIME");

        builder.Property(product => product.DOM)
           .HasColumnType("DATETIME");

        builder.Property(product => product.FeatureImage)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);
    }
}