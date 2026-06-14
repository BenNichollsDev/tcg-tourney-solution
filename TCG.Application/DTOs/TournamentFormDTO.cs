/*
Program: Local Games Store Management System
Filename: TournamentFormDTO.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.DTOs
{
    public class TournamentFormDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Game { get; set; } = string.Empty;

        public string Format { get; set; } = string.Empty;

        public bool RequireDecklist { get; set; }

        public string PairingFormat { get; set; } = "Swiss";

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
    }
}

