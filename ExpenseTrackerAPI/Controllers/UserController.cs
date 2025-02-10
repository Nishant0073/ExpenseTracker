using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController: ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Test");
    }
}