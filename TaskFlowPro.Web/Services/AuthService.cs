using TaskFlowPro.Web.Models;
using TaskFlowPro.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskFlowPro.Web.Services;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<User?> RegisterAsync(string username, string email, string password);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        // For demo purposes, we use simple password check. In real app, use hashing.
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        return user;
    }

    public async Task<User?> RegisterAsync(string username, string email, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Email == email))
            return null;

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = password // In real app, hash this!
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
