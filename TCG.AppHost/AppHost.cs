//
// Program: Local Games Store Management System
// Filename: AppHost.cs
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
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("database-server")
    .WithDataVolume()
    .WithPgAdmin();

var db = postgres.AddDatabase("db-application");

builder.AddProject<Projects.TCG_EMS>("tcg-ems")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.TCG_Website>("tcg-website")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();

