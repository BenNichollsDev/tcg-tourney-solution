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
using Microsoft.AspNetCore.Mvc;
using TCG.Application.Models;
using TCG.EMS.Services;

namespace TCG.EMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginAuthService _authService;
    private readonly StaffSessionService _sessionService;

    public AuthController(LoginAuthService authService, StaffSessionService sessionService)
    {
        _authService = authService;
        _sessionService = sessionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { success = false, message = "An email and password are required" });
        }

        try
        {
            var staff = await _authService.AuthenticateAsync(request.Email, request.Password);

            if (staff == null)
            {
                return Unauthorized(new { success = false, message = "Invalid email or password." });
            }

            var principal = _authService.GetPrincipal(staff);

            await _sessionService.SignInAsync(principal);

            return Ok(new { success = true, message = "Login successful", staffId = staff.StaffId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = "An error occurred during login", error = ex.Message });
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _sessionService.LogoutAsync();
            return Ok(new { success = true, message = "Logout successful" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = "An error occurred during logout", error = ex.Message });
        }
    }

    [HttpGet("current-user")]
    public IActionResult GetCurrentUser()
    {
        if (!_sessionService.IsLoggedIn)
        {
            return Ok(new { isLoggedIn = false });
        }

        return Ok(new
        {
            isLoggedIn = true,
            staffId = _sessionService.CurrentStaffId,
            email = _sessionService.CurrentEmail,
            name = _sessionService.CurrentName
        });
    }
}
