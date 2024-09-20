using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FRESHY.Authentication.Application.Interfaces.Persistance;
using Microsoft.AspNetCore.Identity;

namespace FRESHY.Authentication.Infrastructure.Persistance.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FreshyAuthDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(FreshyAuthDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddNewEmployeeAccount(IdentityUser employee, string password, ICollection<string> roles)
        {
            var identityResult = await _userManager.CreateAsync(employee, password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(employee, roles);
                if (identityResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteAccount(Guid accountId)
        {
            var deletedUser = await _userManager.FindByIdAsync(accountId.ToString());
            if (deletedUser != null)
            {
                await _userManager.DeleteAsync(deletedUser);
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAllCustomerAccounts()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            return customers;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllEmployeeAccounts()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var superAdmins = await _userManager.GetUsersInRoleAsync("SuperAdmin");
            employees = employees.Except(superAdmins).ToList();
            return employees;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllAdminAccounts()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var superAdmins = await _userManager.GetUsersInRoleAsync("SuperAdmin");
            admins = admins.Except(superAdmins).ToList();
            return admins;
        }
       
        public async Task<bool> RegisterUserAsync(IdentityUser user, string password, ICollection<string> roles)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                await _userManager.AddToRolesAsync(user, roles);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserAsync(IdentityUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AssignRolesToUserAsync(IdentityUser user, ICollection<string> roles)
        {
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRolesFromUserAsync(IdentityUser user, ICollection<string> roles)
        {
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

    }
}
