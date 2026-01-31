// References:
// [1]: Microsoft (2026) aspnetcore(Version 10.0.2). [Source Code] Available from: https://github.com/dotnet/aspnetcore [Accessed 30/01/2026].

using Microsoft.AspNetCore.Authentication.Cookies;
using tcg.ems.Components;

namespace tcg.ems
{
    //<-***** Microsoft (2026) [1] - START
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "TCGTSAuthCookie";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.Cookie.MaxAge = TimeSpan.FromHours(2);
                    options.AccessDeniedPath = "/login";
                });

            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
//<-***** Microsoft (2026) [1] - END