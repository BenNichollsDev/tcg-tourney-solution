//
// Program: Local Games Store Management System
// Filename: Program.cs
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

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Application.Services;
using TCG.Domain.Entities;
using TCG.EMS.Components;
using TCG.EMS.Interfaces;
using TCG.EMS.Services;
using TCG.Infrastructure;
using TCG.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString =
    builder.Configuration.GetConnectionString("db-application");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure();
            npgsqlOptions.CommandTimeout(180);
        }
    )
);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();

builder.Services.AddSingleton<TCG.EMS.Services.AppBusyService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.Cookie.Name = "TCGTourneys.Auth";
        options.Cookie.HttpOnly = true;

        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPairingService, PairingService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITournamentPlayerService, TournamentPlayerService>();

builder.Services.AddScoped<ITournamentScoringService, TournamentScoringService>();
builder.Services.AddScoped<IPasswordHasher<Staff>, PasswordHasher<Staff>>();

builder.Services.AddScoped<INavigationService, NavigationService>();
builder.Services.AddScoped<StaffSessionService>();
builder.Services.AddScoped(typeof(TCG.Application.Services.LoginAuthService));
builder.Services.AddScoped(typeof(TCG.Application.Services.LoginService));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, Microsoft.AspNetCore.Components.Server.ServerAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    // Runs SQL script on environment startup if the database is empty
    bool hasAnything;
    var conn = dbContext.Database.GetDbConnection();
    try
    {
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT
                EXISTS(SELECT 1 FROM public.staff)
             OR EXISTS(SELECT 1 FROM public.tournaments)
             OR EXISTS(SELECT 1 FROM public.tournament_players)
             OR EXISTS(SELECT 1 FROM public.pairings)
             OR EXISTS(SELECT 1 FROM public.players)
             OR EXISTS(SELECT 1 FROM public.leagues);
        ";
        var result = cmd.ExecuteScalar();
        hasAnything = result is bool b && b;
    }
    finally
    {
        conn.Close();
    }

    if (!hasAnything)
    {
        var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
        var possiblePaths = new[]
        {
            System.IO.Path.Combine(env.ContentRootPath, "Data", "init.sql"),
            System.IO.Path.Combine(env.ContentRootPath, "init.sql")
        };

        string? initPath = null;
        foreach (var file in possiblePaths)
        {
            if (System.IO.File.Exists(file))
            {
                initPath = file;
                break;
            }
        }

        if (!string.IsNullOrEmpty(initPath))
        {
            var sql = System.IO.File.ReadAllText(initPath);
            if (!string.IsNullOrWhiteSpace(sql))
            {
                dbContext.Database.ExecuteSqlRaw(sql);
            }
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/_404", createScopeForStatusCodePages: true);

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();

