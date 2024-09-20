using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    private const string CART_TABLE = "Carts";
    private const string SCHEMA = "customer";

    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        ConfigCartTable(builder);
        AddCustomerForeignKey(builder);
    }

    private static void AddCustomerForeignKey(EntityTypeBuilder<Cart> builder)
    {
        builder.HasOne(cart => cart.Customer)
            .WithOne(customer => customer.Cart)
            .HasForeignKey<Cart>(cart => cart.CustomerId);
    }

    private static void ConfigCartTable(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(CART_TABLE, SCHEMA);

        builder.HasKey(cart => cart.Id);

        builder.Property(cart => cart.Id)
            .HasConversion(id => id.Value, value => CartId.Create(value))
            .ValueGeneratedNever();
    }
}