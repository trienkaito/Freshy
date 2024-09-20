using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    private const string CART_ITEM_TABLE = "CartItems";
    private const string SCHEMA = "customer";

    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        ConfigCartItems(builder);
        AddCartForeignKey(builder);
        AddProductForeignKey(builder);
        AddProductUnitForeinKey(builder);
    }

    private static void AddProductUnitForeinKey(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasOne(item => item.ProductUnit)
            .WithMany(unit => unit.CartItems)
            .HasForeignKey(item => item.ProductUnitId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void AddProductForeignKey(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasOne(item => item.Product)
            .WithMany(product => product.CartItems)
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void AddCartForeignKey(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasOne(item => item.Cart)
            .WithMany(cart => cart.CartItems)
            .HasForeignKey(item => item.CartId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void ConfigCartItems(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(CART_ITEM_TABLE, SCHEMA);

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Id)
            .HasConversion(id => id.Value, value => CartItemId.Create(value))
            .ValueGeneratedNever();

        builder.Property(item => item.BoughtQuantity)
            .HasColumnType("INT");

        builder.Property(item => item.TotalPrice)
            .HasColumnType("MONEY");
    }
}