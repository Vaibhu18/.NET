using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService service;
    public AuthController(IAuthService service)
    {
        this.service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User?>> Register(UserDto request)
    {
        var user = await service.RegisterAsync(request);
        if (user is null) return BadRequest("User already exists");
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
    {
        var token = await service.LoginAsync(request);
        if (token is null) return BadRequest("Invalid credentials");
        return Ok(token);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
    {
        var token = await service.RefreshTokenAsync(request);
        if (token is null) return BadRequest("invalid/expired refresh token");
        return Ok(token);
    }

    [HttpGet("Auth-endpoint")]
    [Authorize] // which is setuped in program.cs
    public ActionResult AuthCheck()
    {
        return Ok();
    }

    [HttpGet("Admin-endpoint")]
    [Authorize(Roles = "Admin")] // which is setuped in program.cs
    public ActionResult AdminCheck()
    {
        return Ok();
    }
}