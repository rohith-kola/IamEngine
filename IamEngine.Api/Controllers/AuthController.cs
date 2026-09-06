using IamEngine.Application.DTOs;
using IamEngine.Application.Interfaces;
using IamEngine.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IamEngine.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupRequest req)
    {
        var user = new AppUser
        {
            UserName = req.Email,
            Email = req.Email,
            DisplayName = req.DisplayName,
            Source = UserSource.Local,
            CreatedAtUtc = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors.Select(e => e.Description));

        var token = await _tokenService.GenerateJwtAsync(user);
        return Ok(new AuthResponse(token, user.Email!, user.DisplayName));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user is null || !user.IsActive)
            return Unauthorized(new { message = "Invalid credentials" });

        var check = await _signInManager.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
        if (!check.Succeeded)
            return Unauthorized(new { message = "Invalid credentials" });

        var token = await _tokenService.GenerateJwtAsync(user);
        return Ok(new AuthResponse(token, user.Email!, user.DisplayName));
    }

    [HttpGet("me")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("sub")?.Value;
        var user = await _userManager.FindByIdAsync(userId!);
        if (user is null) return NotFound();

        return Ok(new { user.Email, user.DisplayName, user.Source, user.CreatedAtUtc });
    }
}