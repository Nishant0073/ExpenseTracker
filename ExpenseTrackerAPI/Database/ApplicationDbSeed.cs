using ExpenseTrackerAPI.Constants;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerAPI.Database;

public class ApplicationDbSeed
{

    public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        roleManager.CreateAsync(new IdentityRole(AuthConstants.Roles.Admin.ToString()));
        roleManager.CreateAsync(new IdentityRole(AuthConstants.Roles.User.ToString()));
       
        var defaultUser = new IdentityUser {UserName = AuthConstants.defaultUsername, Email = AuthConstants.defaultEmail, EmailConfirmed = true,PhoneNumberConfirmed = true};

        if (userManager.Users.All(u => u.Email != AuthConstants.defaultEmail))
        {
            await userManager.CreateAsync(defaultUser, AuthConstants.defaultPassword);
            await userManager.AddToRoleAsync(defaultUser, AuthConstants.Roles.Admin.ToString());
        }
    }
}