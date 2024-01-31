using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("user")]
    [Authorize]
    public IActionResult GetUser()
    {
  
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(ClaimTypes.Name);

        var userClaims = User.Claims.Select(c => new { c.Type, c.Value });

        return Ok(new
        {
            UserId = userId,
            Username = username,
            Claims = userClaims
        });
    }
}
    