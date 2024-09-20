using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    private const string ORDER_ITEM_TABLE = "ORDER_ITEMS";
    private const string SCHEMA = "order";

    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        ConfigOrderItems(builder);
        AddOrderDetailsForeignKey(builder);
        AddProductForeignKey(builder);
        AddProductUnitForeinKey(builder);
    }

    private static void AddProductUnitForeinKey(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(item => item.ProductUnit)
            .WithMany(unit => unit.OrderItems)
            .HasForeignKey(item => item.ProductUnitId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void AddProductForeignKey(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(item => item.Product)
            .WithMany(product => product.OrderItems)
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void AddOrderDetailsForeignKey(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(item => item.OrderDetail)
            .WithMany(order => order.OrderItems)
            .HasForeignKey(item => item.OrderDetailId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void ConfigOrderItems(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(ORDER_ITEM_TABLE, SCHEMA);

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Id)
            .HasConversion(id => id.Value, value => OrderItemId.Create(value))
            .ValueGeneratedNever();

        builder.Property(order => order.BoughtQuantity)
                .HasColumnName("BoughtQuantity")
                .HasColumnType("INT");

        builder.Property(item => item.TotalPrice)
                .HasColumnName("TotalProductPrice")
                .HasColumnType("MONEY");
    }
}