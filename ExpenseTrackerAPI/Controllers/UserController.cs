using ExpenseTrackerAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers;

[ApiController]
[Route("[controller]/")]
public class UserController: ControllerBase
{
    [Authorize]
    [HttpPost("token")]
    public IActionResult Token([FromBody] LoginModel loginModel)
    {
        return Ok("Test");
    }
}