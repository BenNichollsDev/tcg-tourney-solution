//
// Program: Local Games Store Management System
// Filename: StaffSessionService.cs
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
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TCG.Application.Dtos;

namespace TCG.EMS.Services;

/// <summary>
/// Service for managing the current staff session and authentication state.
/// </summary>
public class StaffSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationStateProvider _authStateProvider;

    public StaffSessionService(IHttpContextAccessor httpContextAccessor, AuthenticationStateProvider authStateProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _authStateProvider = authStateProvider;
    }

    public int? CurrentStaffId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var staffIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (int.TryParse(staffIdClaim?.Value, out var staffId))
                {
                    return staffId;
                }
            }
            return null;
        }
    }

    public int? StaffId => CurrentStaffId;

    public string? CurrentEmail
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                return user.FindFirst(ClaimTypes.Email)?.Value;
            }
            return null;
        }
    }

    public string? CurrentName
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                return user.FindFirst(ClaimTypes.GivenName)?.Value ?? user.Identity?.Name;
            }
            return null;
        }
    }

    public bool IsLoggedIn => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public async Task<bool> IsLoggedInAsync()
        {
            try
            {
                var state = await _authStateProvider.GetAuthenticationStateAsync();
                return state.User?.Identity?.IsAuthenticated == true;
            }
            catch
            {
                return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            }
        }

        public async Task<int?> GetCurrentStaffIdAsync()
        {
            try
            {
                var state = await _authStateProvider.GetAuthenticationStateAsync();
                var user = state.User;
                if (user?.Identity?.IsAuthenticated == true)
                {
                    var staffIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                    if (int.TryParse(staffIdClaim?.Value, out var staffId))
                        return staffId;
                    else
                        throw new Exception();
                }

                throw new Exception();
            }
            catch
            {
                var httpUser = _httpContextAccessor.HttpContext?.User;
                if (httpUser?.Identity?.IsAuthenticated == true)
                {
                    var staffIdClaim = httpUser.FindFirst(ClaimTypes.NameIdentifier);
                    if (int.TryParse(staffIdClaim?.Value, out var staffId))
                        return staffId;
                }
            }

            return null;
        }

        public async Task LogoutAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions
                .SignOutAsync(httpContext, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

    public async Task SignInAsync(ClaimsPrincipal principal)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions
                .SignInAsync(httpContext, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
