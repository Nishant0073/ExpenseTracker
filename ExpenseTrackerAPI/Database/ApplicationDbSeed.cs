using ExpenseTrackerAPI.Constants;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerAPI.Database;

public class ApplicationDbSeed
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(AuthConstants.Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AuthConstants.Roles.User.ToString()));
       
        var defaultUser = new IdentityUser {UserName = AuthConstants.defaultUsername, Email = AuthConstants.defaultEmail, EmailConfirmed = true,PhoneNumberConfirmed = true};

        if (userManager.Users.All(u => u.Email != AuthConstants.defaultEmail))
        {
            try
            {
                var isUserCreated = await userManager.CreateAsync(defaultUser, AuthConstants.defaultPassword);
                if (isUserCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, AuthConstants.Roles.Admin.ToString());
                }
                else
                {
                    throw new Exception(isUserCreated.Errors.FirstOrDefault().Description);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
