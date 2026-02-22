using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Services;
using TCG.Domain.Entities;
using TCG.EMS.Components;
using TCG.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//// Ensure Kestrel binds explicit HTTP and HTTPS ports so localhost:5001 is available
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenLocalhost(5000); // HTTP
//    options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps()); // HTTPS (uses dev certificate)
//});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// <-***** Microsoft (2026) [1] - END

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
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

// <-***** Microsoft (2026) [1] - START

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// <-***** Microsoft (2026) [1] - END

app.UseStatusCodePagesWithReExecute("/_404", createScopeForStatusCodePages: true);

// IMPORTANT: Authentication & Authorization MUST come before endpoint mapping
// <-***** Anderson, R. (2025) [2] - START
app.UseAuthentication();
app.UseAuthorization();
// <-***** Anderson, R. (2025) [2] - END

// <-***** Microsoft (2026) [1] - START
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
// <-***** Microsoft (2026) [1] - END

app.MapControllers();

app.Run();