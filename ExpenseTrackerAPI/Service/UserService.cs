using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTrackerAPI.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTrackerAPI.Service;

public class UserService : IUserService
{
    private UserManager<IdentityUser> _userManager;
    private IConfiguration _configuration;

    public UserService(UserManager<IdentityUser> userManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GetToken(LoginModel loginModel)
    {
        IdentityUser? user = await _userManager.FindByEmailAsync(loginModel.email);
        if (user == null)
        {
            return "User not found";
        }

        if (await _userManager.CheckPasswordAsync(user, loginModel.password))
        {
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        return "Invalid password";
    }

    public async Task<string> CreateUser(LoginModel loginModel)
    {
        IdentityUser? user = await _userManager.FindByEmailAsync(loginModel.email);
        if (user != null)
        {
            return "User with the same email already exists";
        }

        try
        {
            user = new IdentityUser { Email = loginModel.email, UserName = loginModel.email, EmailConfirmed = true };
            var isUserCreated = await _userManager.CreateAsync(user, loginModel.password);
            if (isUserCreated.Succeeded)
            {
                return "User created successfully";
            }

            return "User creation failed";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string> AddRole(AddRoleModel addRoleModel)
    {
        var user = _userManager.FindByEmailAsync(addRoleModel.email).Result;
        if(user == null)
            return "User not found";
        if (await _userManager.CheckPasswordAsync(user, addRoleModel.password))
        {
            var result = await _userManager.AddToRoleAsync(user, addRoleModel.Role.ToString());
            if (result.Succeeded)
            {
                return "Role added successfully";
            }
            return $"Role added failed {result.Errors.FirstOrDefault().Description}";
        }
        return "Invalid password";
    }

    private async Task<JwtSecurityToken> CreateJwtToken(IdentityUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", user.Id),
        }.Union(userClaims).Union(roleClaims);
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
        return jwtSecurityToken;
        
    }
}