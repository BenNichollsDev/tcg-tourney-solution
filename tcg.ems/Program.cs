using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Services;
using TCG.Domain.Entities;
using TCG.EMS.Components;
using TCG.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var dbString = Environment.GetEnvironmentVariable("ConnectionStrings__db-application");
if (dbString is null) throw new NullReferenceException(nameof(dbString));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        dbString,
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddAuthentication()
    .AddCookie("default", options =>
    {
        options.LoginPath = "/login";
        options.Cookie.Name = "TCGAuth";
        options.LogoutPath = "/auth/logout";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<
    IGenericService<Tournament, TournamentDto>,
    GenericService<Tournament, TournamentDto>>();

builder.Services.AddScoped<
    IGenericService<Staff, StaffDto>,
    GenericService<Staff, StaffDto>>();

builder.Services.AddScoped<LoginAuthService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorizationCore();
builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

var app = builder.Build();

app.MapDefaultEndpoints();

/* 🔥 AUTOMATIC MIGRATIONS — THIS FIXES YOUR ERROR */
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/_404", createScopeForStatusCodePages: true);

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();