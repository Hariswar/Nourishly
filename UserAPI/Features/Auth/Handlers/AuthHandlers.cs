using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Features.Auth.Commands;

namespace UserAPI.Features.Auth.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, object>
{
    private readonly UserContext _context;

    public RegisterHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Auths.AnyAsync(a => a.Username == request.Username, cancellationToken))
            throw new InvalidOperationException("Username already exists");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var auth = new Models.Auth
        {
            Username = request.Username,
            Password = request.Password
        };
        _context.Auths.Add(auth);
        await _context.SaveChangesAsync(cancellationToken);

        await _context.Database.ExecuteSqlAsync(
            $"INSERT INTO user_auth_map (user_id, auth_id) VALUES ({user.UserId}, {auth.AuthId})",
            cancellationToken);

        return new { userId = user.UserId, name = $"{user.FirstName} {user.LastName}", email = user.Email };
    }
}

public class LoginHandler : IRequestHandler<LoginCommand, object?>
{
    private readonly UserContext _context;

    public LoginHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<object?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var auth = await _context.Auths.FirstOrDefaultAsync(
            a => a.Username == request.Username && a.Password == request.Password, 
            cancellationToken);
        
        if (auth == null) return null;

        var userAuthMap = await _context.Database
            .SqlQuery<UserAuthMap>($"SELECT user_id AS \"UserId\", auth_id AS \"AuthId\" FROM user_auth_map WHERE auth_id = {auth.AuthId}")
            .FirstOrDefaultAsync(cancellationToken);

        if (userAuthMap == null) return null;

        var user = await _context.Users.FindAsync(new object[] { userAuthMap.UserId }, cancellationToken);
        if (user == null) return null;

        return new { userId = user.UserId, name = $"{user.FirstName} {user.LastName}", email = user.Email };
    }
}

public class UserAuthMap { public int UserId { get; set; } public int AuthId { get; set; } }
