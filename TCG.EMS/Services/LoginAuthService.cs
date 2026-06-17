//
// Program: Local Games Store Management System
// Filename: LoginAuthService.cs
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
using TCG.Application.Dtos;
using TCG.Application.Interfaces.Services;

namespace TCG.EMS.Services;

public class LoginAuthService : IAuthenticationService
{
    private readonly IStaffService _staffService;

    public LoginAuthService(IStaffService staffService)
    {
        _staffService = staffService;
    }

    public async Task<StaffDto?> AuthenticateAsync(string email, string password)
    {
        try
        {
            var staff = await _staffService.GetByAsync(s => s.StaffEmail == email);

            if (staff == null)
                return null;

            var isPasswordValid = await _staffService.VerifyPasswordAsync(staff.StaffId, password);

            if (!isPasswordValid)
                return null;

            return staff;
        }
        catch
        {
            return null;
        }
    }

    public ClaimsPrincipal GetPrincipal(StaffDto staff)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, staff.StaffId.ToString()),
            new Claim(ClaimTypes.Email, staff.StaffEmail),
            new Claim("StaffFirstName", staff.StaffFirstName),
            new Claim("StaffSurname", staff.StaffSurname),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        return new ClaimsPrincipal(claimsIdentity);
    }
}
