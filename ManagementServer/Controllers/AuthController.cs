using Microsoft.AspNetCore.Mvc;
using ManagementServer.Models;
using ManagementServer.Services;

namespace ManagementServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("validate-admin-code")]
    public ActionResult<AuthResponse> ValidateAdminCode([FromBody] RegisterRequest request)
    {
        bool valid = _authService.ValidateAdminCode(request.AdminCode);

        return Ok(new AuthResponse
        {
            Success = valid,
            Message = valid
                ? "Admin kód elfogadva."
                : "Érvénytelen admin kód. Keresd fel a rendszergazdát."
        });
    }

    [HttpPost("register")]
    public ActionResult<AuthResponse> Register([FromBody] RegisterRequest request)
    {
        var result = _authService.Register(
            request.AdminCode,
            request.FullName,
            request.Username,
            request.Password);

        return Ok(new AuthResponse
        {
            Success = result.Success,
            Message = result.Message
        });
    }

    [HttpPost("login")]
    public ActionResult<AuthResponse> Login([FromBody] LoginRequest request)
    {
        var result = _authService.Login(request.Username, request.Password);

        return Ok(new AuthResponse
        {
            Success = result.Success,
            Message = result.Message,
            Token = result.Token,
            FullName = result.FullName,
            Username = result.Username
        });
    }

    [HttpPost("logout")]
    public ActionResult<AuthResponse> Logout([FromHeader(Name = "X-Auth-Token")] string token)
    {
        _authService.Logout(token);

        return Ok(new AuthResponse
        {
            Success = true,
            Message = "Sikeres kijelentkezés."
        });
    }
}