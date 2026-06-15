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
namespace TCG.EMS.Services;

public class StaffSessionService
{
    private int? _staffId;
    private string? _staffName;
    private string? _staffEmail;

    public int? StaffId
    {
        get => _staffId;
        set => _staffId = value;
    }

    public string? StaffName
    {
        get => _staffName;
        set => _staffName = value;
    }

    public string? StaffEmail
    {
        get => _staffEmail;
        set => _staffEmail = value;
    }

    public bool IsLoggedIn => StaffId.HasValue && !string.IsNullOrWhiteSpace(StaffName);

    public void Logout()
    {
        _staffId = null;
        _staffName = null;
        _staffEmail = null;
    }
}


