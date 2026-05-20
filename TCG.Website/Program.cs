using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.EntityFrameworkCore;
using TCG.Infrastructure;
using TCG.Website.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

if (builder.Environment.IsDevelopment())
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

var dbString = Environment.GetEnvironmentVariable("ConnectionStrings__db-application")
               ?? "Host=localhost;Port=5432;Database=tcg_db;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        dbString,
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
    )
);

var app = builder.Build();

app.MapDefaultEndpoints();

/* 🔥 AUTOMATIC MIGRATIONS */
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();