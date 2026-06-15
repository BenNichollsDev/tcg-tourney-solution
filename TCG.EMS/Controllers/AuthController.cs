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
            // Handle failed login attempt (e.g., return an error message)

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

