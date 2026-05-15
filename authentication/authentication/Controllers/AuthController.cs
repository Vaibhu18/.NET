using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public AuthController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    private static User? user = new();
    private readonly IConfiguration configuration;

    [HttpPost("register")]
    public ActionResult<User?> Register(UserDto request)
    {
        user.Username = request.Username;
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
        return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult<string> Login(UserDto request)
    {
        if (user.Username != request.Username)
            return BadRequest("User not found");

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            return BadRequest("Invalid password");

        string token = CreateToken(user);
        return token;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}