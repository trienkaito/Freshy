using Microsoft.AspNetCore.Identity;

namespace FRESHY.Authentication.Application.Interfaces.Persistance;

public interface IAccountRepository 
{
    Task<bool> AddNewEmployeeAccount(IdentityUser employee, string password, ICollection<string> roles);

    Task DeleteAccount(Guid accountId);

    Task<IEnumerable<IdentityUser>> GetAllCustomerAccounts();

    Task<IEnumerable<IdentityUser>> GetAllEmployeeAccounts();

    Task<IEnumerable<IdentityUser>> GetAllAdminAccounts();   

    Task<bool> RegisterUserAsync(IdentityUser user, string password, ICollection<string> roles);
    Task<bool> UpdateUserAsync(IdentityUser user);
    Task<bool> AssignRolesToUserAsync(IdentityUser user, ICollection<string> roles);
    Task<bool> RemoveRolesFromUserAsync(IdentityUser user, ICollection<string> roles);
    Task<bool> DeleteUserAsync(string userId);
    Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
    Task<IdentityUser> GetUserByIdAsync(string userId);
    Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user);


}