//
// Program: Local Games Store Management System
// Filename: PlayerSessionService.cs
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
namespace TCG.Website.Services;

public class PlayerSessionService
{
    private int? _playerId;
    private string? _playerName;
    private string? _playerEmail;

    public int? PlayerId
    {
        get => _playerId;
        set => _playerId = value;
    }

    public string? PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }

    public string? PlayerEmail
    {
        get => _playerEmail;
        set => _playerEmail = value;
    }

    public bool IsLoggedIn => PlayerId.HasValue && !string.IsNullOrWhiteSpace(PlayerName);

    public void Logout()
    {
        _playerId = null;
        _playerName = null;
        _playerEmail = null;
    }
}


