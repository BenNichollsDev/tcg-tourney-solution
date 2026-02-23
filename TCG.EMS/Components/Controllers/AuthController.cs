using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TCG.Application.Services;
using TCG.Application.Models;

namespace TCG.EMS.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly LoginService _loginService;

    public AuthController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest request)
    {
        var result = await _loginService.LoginAsync(request.Email, request.Password);

        if (!result.WasSuccess)
            return Redirect("/login?error=invalid");

        await HttpContext.SignInAsync(
            "default",
            result.CPrinciple!
        );

        return Redirect("/home");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("default");
        return Redirect("/login");
    }
}
