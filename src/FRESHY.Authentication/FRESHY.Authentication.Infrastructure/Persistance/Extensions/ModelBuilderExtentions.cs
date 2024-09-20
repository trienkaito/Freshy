using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Authentication.Infrastructure.Persistance.Extensions;

public static class ModelBuilderExtentions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var superAdminRoleId = "8c8ff7dd-589f-4354-b2af-a68934fb1758";
        var adminRoleId = "81de03d7-6093-4a48-9917-5f02dcb5ce00";
        var employeeRoleId = "09adeaeb-d3e7-4256-8d73-87e6d6d50346";
        var customerRoleId = "b7d5bf05-030c-42ea-b1e2-c6df46ed74d4";

        var roles = new List<IdentityRole>
        {
            new IdentityRole()
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            },
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole()
            {
                Name = "Employee",
                NormalizedName = "Employee",
                Id = employeeRoleId,
                ConcurrencyStamp = employeeRoleId
            },
            new IdentityRole()
            {
                Name = "Customer",
                NormalizedName = "Customer",
                Id = customerRoleId,
                ConcurrencyStamp = customerRoleId
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);

        var superAdminUserId = "a1297941-f38a-43da-9b00-4926cf24b3e0";

        var superAdminUser = new IdentityUser()
        {
            Id = superAdminUserId,
            UserName = "superadmin@freshy.com",
            Email = "superadmin@freshy.com",
            NormalizedEmail = "superadmin@freshy.com".ToUpper(),
            NormalizedUserName = "superadmin@freshy.com".ToUpper()
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "freshy@123");

        modelBuilder.Entity<IdentityUser>().HasData(superAdminUser);

        var defaultCustomerAccountId = "41f0043e-5240-4607-9fe4-a891f58dbd43";

        var defaultCustomerAccount = new IdentityUser()
        {
            Id = defaultCustomerAccountId,
            UserName = "DefaultCustomer",
            Email = "default@freshy.com",
            NormalizedEmail = "default@freshy.com".ToUpper(),
            NormalizedUserName = "DefaultCustomer".ToUpper()
        };

        defaultCustomerAccount.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(defaultCustomerAccount, "freshy@123");

        modelBuilder.Entity<IdentityUser>().HasData(defaultCustomerAccount);

        var superAdminRoles = new List<IdentityUserRole<string>>()
        {
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminUserId,
            },
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminUserId,
            },
            new IdentityUserRole<string>
            {
                RoleId = employeeRoleId,
                UserId = superAdminUserId,
            }
        };
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        var defautlCustomerRoles = new List<IdentityUserRole<string>>()
        {
            new IdentityUserRole<string>
            {
                RoleId = customerRoleId,
                UserId = defaultCustomerAccountId
            }
        };
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(defautlCustomerRoles);
    }
}