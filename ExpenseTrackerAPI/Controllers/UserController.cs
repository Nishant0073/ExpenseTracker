using ExpenseTrackerAPI.Models.User;
using ExpenseTrackerAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers;

[ApiController]
[Route("[controller]/")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("token")]
    public async Task<IActionResult> Token([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(await _userService.GetToken(loginModel));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _userService.CreateUser(loginModel));
    }
}