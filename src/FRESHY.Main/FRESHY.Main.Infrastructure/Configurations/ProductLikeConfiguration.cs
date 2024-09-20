using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class ProductLikeConfiguration : IEntityTypeConfiguration<ProductLike>
{
    private const string PRODUCT_LIKE_TABLE = "ProductLikes";
    private const string SCHEMA = "product";

    public void Configure(EntityTypeBuilder<ProductLike> builder)
    {
        ConfigProductLike(builder);
        AddProductForeignKey(builder);
        AddCustomerForeignKey(builder);
    }

    private static void AddCustomerForeignKey(EntityTypeBuilder<ProductLike> builder)
    {
        builder.HasOne(like => like.Product)
            .WithMany(product => product.Likes)
            .HasForeignKey(like => like.ProductId);
    }

    private static void AddProductForeignKey(EntityTypeBuilder<ProductLike> builder)
    {
        builder.HasOne(like => like.Customer)
            .WithMany(customer => customer.Likes)
            .HasForeignKey(like => like.CustomerId);
    }

    private static void ConfigProductLike(EntityTypeBuilder<ProductLike> builder)
    {
        builder.ToTable(PRODUCT_LIKE_TABLE, SCHEMA);
        builder.HasKey(review => review.Id);

        builder.Property(review => review.Id)
            .HasConversion(id => id.Value, value => ProductLikeId.Create(value))
            .ValueGeneratedNever();
    }
}