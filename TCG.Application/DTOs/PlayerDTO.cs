/*
Program: Local Games Store Management System
Filename: PlayerDTO.cs
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

namespace TCG.Application.Dtos
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }

        public string PlayerFirstName { get; set; } = string.Empty;

        public string PlayerLastName { get; set; } = string.Empty;

        public string PlayerEmail { get; set; } = string.Empty;

        public string PlayerPhone { get; set; } = string.Empty;

        public DateOnly PlayerDOB { get; set; }

        public string PlayerGender { get; set; } = string.Empty;

        public string PlayerPassword { get; set; } = string.Empty;
    }
}

