using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductType>().HasData(
            ProductType.Create("Meat"),
            ProductType.Create("Seafood"),
            ProductType.Create("Vegatables"),
            ProductType.Create("Fruits"),
            ProductType.Create("Beverages"),
            ProductType.Create("Dairy products"),
            ProductType.Create("Condiments"),
            ProductType.Create("Combo")
        );
    }
}