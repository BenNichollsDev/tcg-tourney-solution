//
// Program: Local Games Store Management System
// Filename: AuthController.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 14/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using TCG.Application.Services;
using TCG.Application.Models;
using TCG.Application.Dtos;
using TCG.Application.Interfaces.Services;

namespace TCG.Website.Controllers;

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
        // First try staff login (existing LoginService)
        var result = await _loginService.LoginAsync(request.Email, request.Password);

        if (result.WasSuccess)
        {
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                result.CPrinciple!
            );

            return Redirect("/");
        }

        // If staff login failed, attempt player login using IPlayerService
        try
        {
            var playerService = HttpContext.RequestServices.GetRequiredService<IPlayerService>();
            var player = await playerService.GetByAsync(p => p.PlayerEmail == request.Email);

            if (player != null)
            {
                var isValid = await playerService.VerifyPasswordAsync(player.PlayerId, request.Password);
                if (isValid)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, player.PlayerId.ToString()),
                        new Claim(ClaimTypes.Email, player.PlayerEmail ?? string.Empty),
                        new Claim("PlayerName", $"{player.PlayerFirstName} {player.PlayerLastName}")
                    };

                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Redirect("/");
                }
            }
        }
        catch
        {
            // ignore and fallthrough to error redirect
        }

        return Redirect("/login-signup?error=1");
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromForm] PlayerSignupRequest request)
    {
        try
        {
            var playerService = HttpContext.RequestServices.GetRequiredService<IPlayerService>();
            var playerDto = new PlayerDto
            {
                PlayerFirstName = request.PlayerFirstName ?? string.Empty,
                PlayerLastName = request.PlayerLastName ?? string.Empty,
                PlayerEmail = request.PlayerEmail ?? string.Empty,
                PlayerPhone = request.PlayerPhone ?? string.Empty,
                PlayerGender = string.Empty
            };

            if (!string.IsNullOrWhiteSpace(request.PlayerDOB) && DateTime.TryParse(request.PlayerDOB, out var dob))
            {
                playerDto.PlayerDOB = DateOnly.FromDateTime(dob);
            }

            var newPlayer = await playerService.CreateWithDefaultPasswordAsync(playerDto);

            // Try to log the user in using the default password the service set when creating the account.
            // The PlayerService uses default password "123" when creating accounts.
            var result = await _loginService.LoginAsync(request.PlayerEmail, "123");

            if (!result.WasSuccess)
            {
                return Redirect($"/login-signup?signupError=Account created but automatic login failed. Please login manually.");
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                result.CPrinciple!
            );

            return Redirect("/");
        }
        catch (System.ComponentModel.DataAnnotations.ValidationException vex)
        {
            // Email or phone uniqueness validation failed
            return Redirect($"/login-signup?signupError={System.Net.WebUtility.UrlEncode(vex.Message)}");
        }
        catch (Exception)
        {
            return Redirect("/login-signup?error=1");
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("default");
        return Redirect("/login-signup");
    }
}

public class PlayerSignupRequest
{
    public string PlayerFirstName { get; set; } = string.Empty;
    public string PlayerLastName { get; set; } = string.Empty;
    public string PlayerEmail { get; set; } = string.Empty;
    public string? PlayerPhone { get; set; }
    public string? PlayerDOB { get; set; }
    public string? PlayerGender { get; set; }
}

