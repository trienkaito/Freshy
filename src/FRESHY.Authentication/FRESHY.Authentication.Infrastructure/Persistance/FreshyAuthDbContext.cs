using FRESHY.Authentication.Infrastructure.Persistance.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Authentication.Infrastructure.Persistance;

public class FreshyAuthDbContext : IdentityDbContext
{
    public FreshyAuthDbContext(DbContextOptions<FreshyAuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Seed();
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}