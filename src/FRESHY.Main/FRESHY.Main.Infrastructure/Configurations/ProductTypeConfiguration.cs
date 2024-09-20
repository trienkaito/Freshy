using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    private const string TYPE_TABLE = "ProductTypes";
    private const string SCHEMA = "product";

    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        ConfigProductType(builder);
    }

    private static void ConfigProductType(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable(TYPE_TABLE, SCHEMA);

        builder.HasKey(type => type.Id);

        builder.Property(type => type.Id)
            .HasConversion(id => id.Value, value => ProductTypeId.Create(value))
            .ValueGeneratedNever();

        builder.Property(type => type.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        builder.HasIndex(type => type.Name)
            .IsUnique();
    }
}