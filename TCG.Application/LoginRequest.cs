/*
Program: Local Games Store Management System
Filename: LoginRequest.cs
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

namespace TCG.Application.Models
{
    public sealed class LoginRequest
    {
        public string Email { get; init; } = "";
        public string Password { get; init; } = "";
    }
}
