using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using FRESHY.Main.Infrastructure.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance;

public class FreshyDbContext : BaseDbContext<FreshyDbContext>
{


    public FreshyDbContext(
        DbContextOptions<FreshyDbContext> options,
        EventInterceptor eventInterceptor) : base(options, eventInterceptor)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<JobPosition> JobPositions { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Cart> Cart { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderAddress> OrderAddresses { get; set; } = null!;
    public DbSet<ProductUnit> Units { get; set; } = null!;
    public DbSet<ProductLike> Likes { get; set; } = null!;
    public DbSet<Voucher> Vouchers { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Shipping> Shippings { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductType> Types { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FreshyDbContext).Assembly);
    }
}