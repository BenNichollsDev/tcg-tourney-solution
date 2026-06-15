//
// Program: Local Games Store Management System
// Filename: IAuthenticationService.cs
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

using TCG.Application.Dtos;

namespace TCG.EMS.Services;

public interface IAuthenticationService
{
    Task<StaffDto?> AuthenticateAsync(string email, string password);

    System.Security.Claims.ClaimsPrincipal GetPrincipal(StaffDto staff);
}
