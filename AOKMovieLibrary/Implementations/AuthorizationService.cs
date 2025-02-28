using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AOKMovieLibrary.Implementations;

public class AuthorizationService : IAuthorizationService
{
    private readonly MovieContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(IDbContextFactory<MovieContext> contextFactory, IHttpContextAccessor httpContextAccessor)
    {
        _context = contextFactory.CreateDbContext();
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> RegisterAsync(RegisterCommand command)
    {
        // Prüfen, ob User bereits existiert
        if (await _context.Users.AnyAsync(u => u.Username == command.Username))
            return false; // User schon vorhanden

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);

        var user = new User
        {
            Username = command.Username,
            PasswordHash = hashedPassword,
            Email = command.Email,
            Firstname = command.Firstname,
            Lastname = command.Lastname
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> LoginAsync(LoginCommand command)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == command.Username);
        if (user == null)
            return false;

        bool isValid = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);
        if (!isValid)
            return false;

        // Benutzer ist authentifiziert, Claims erstellen
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        return true;
    }
    public async Task LogoutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
