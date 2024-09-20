using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    private const string REVIEW_TABLE = "Reviews";
    private const string SCHEMA = "review";

    public void Configure(EntityTypeBuilder<Review> builder)
    {
        ConfigReview(builder);
        AddEmployeeForeignKey(builder);
        AddCustomerForeignKey(builder);
        AddProductForeignKey(builder);
    }

    private static void AddProductForeignKey(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(review => review.Product)
           .WithMany(product => product.Reviews)
           .HasForeignKey(review => review.ProductId);
    }

    private static void AddEmployeeForeignKey(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(review => review.Employee)
           .WithMany(employee => employee.Reviews)
           .HasForeignKey(review => review.EmployeeId)
           .IsRequired(false);
    }

    private static void AddCustomerForeignKey(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(review => review.Customer)
           .WithMany(employee => employee.Reviews)
           .HasForeignKey(review => review.CustomerId)
           .IsRequired(false);
    }

    private static void ConfigReview(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(REVIEW_TABLE, SCHEMA);

        builder.HasKey(review => review.Id);

        builder.Property(review => review.Id)
            .HasConversion(id => id.Value, value => ReviewId.Create(value))
            .ValueGeneratedNever();

        builder.Property(review => review.Content)
            .HasColumnType("NVARCHAR");

        builder.Property(review => review.CreatedDate)
            .HasColumnType("DATETIME");

        builder.Property(review => review.RatingValue)
            .HasColumnType("INT"); //TODO: Add range to it!

        builder.Property(review => review.LikeCount)
            .HasColumnType("INT");

        builder.Property(review => review.IsBeingReply)
            .HasColumnType("BIT");

        builder.Property(review => review.ParentReviewId)
            .HasConversion(
                    id => id.Value,
                    value => ReviewId.Create(value))
            .ValueGeneratedNever()
            .IsRequired(false);
    }
}