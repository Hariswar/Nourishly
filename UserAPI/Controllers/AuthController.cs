using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;

namespace UserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserContext _context;

    public AuthController(UserContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (await _context.Auths.AnyAsync(a => a.Username == request.Username))
            return BadRequest(new { message = "Username already exists" });

        var nameParts = request.Name.Split(' ', 2);
        var user = new User
        {
            FirstName = nameParts[0],
            LastName = nameParts.Length > 1 ? nameParts[1] : "",
            Email = request.Email
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var auth = new Auth
        {
            Username = request.Username,
            Password = request.Password
        };
        _context.Auths.Add(auth);
        await _context.SaveChangesAsync();

        await _context.Database.ExecuteSqlRawAsync(
            "INSERT INTO user_auth_map (user_id, auth_id) VALUES ({0}, {1})",
            user.UserId, auth.AuthId);

        return Ok(new { userId = user.UserId, name = $"{user.FirstName} {user.LastName}", email = user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var auth = await _context.Auths.FirstOrDefaultAsync(a => a.Username == request.Username && a.Password == request.Password);
        
        if (auth == null)
            return Unauthorized(new { message = "Invalid email or password" });

        var userAuthMap = await _context.Database
            .SqlQuery<UserAuthMap>($"SELECT user_id AS \"UserId\", auth_id AS \"AuthId\" FROM user_auth_map WHERE auth_id = {auth.AuthId}")
            .FirstOrDefaultAsync();

        if (userAuthMap == null)
            return Unauthorized(new { message = "User not found" });

        var user = await _context.Users.FindAsync(userAuthMap.UserId);

        return Ok(new { userId = user!.UserId, name = $"{user.FirstName} {user.LastName}", email = user.Email });
    }
}

public record RegisterRequest(string Name, string Email, string Username, string Password);
public record LoginRequest(string Username, string Password);
public class UserAuthMap { public int UserId { get; set; } public int AuthId { get; set; } }
