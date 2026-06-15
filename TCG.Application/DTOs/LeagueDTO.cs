//
// Program: Local Games Store Management System
// Filename: LeagueDTO.cs
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
using System;

namespace TCG.Application.Dtos
{
    public class LeagueDto
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public string LeagueGame { get; set; } = string.Empty;

        public string LeagueDescription { get; set; } = string.Empty;
    }
}

