using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations.CustomerAggregate;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    private const string CUSTOMER_TABLE = "Customers";
    private const string SCHEMA = "customer";

    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        ConfigCustomer(builder);
    }

    private static void ConfigCustomer(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(CUSTOMER_TABLE, SCHEMA);

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
            .HasConversion(id => id.Value, value => CustomerId.Create(value))
            .ValueGeneratedNever();

        builder.Property(customer => customer.Email);
        builder.HasIndex(customer => customer.Email)
            .IsUnique();

        builder.Property(customer => customer.Phone)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        builder.HasIndex(customer => customer.Phone)
            .IsUnique();

        builder.Property(customer => customer.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(customer => customer.Avatar)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

        builder.Property(customer => customer.CreatedDate)
            .HasColumnType("DATETIME");
    }
}