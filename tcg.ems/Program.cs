// References:
// [1]: Microsoft (2026) aspnetcore(Version 10.0.2). [Source Code] Available from: https://github.com/dotnet/aspnetcore [Accessed 30/01/2026].

using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TCG.EMS.Components;
using TCG.Application.Services;
using TCG.Application.Interfaces;
using TCG.Infrastructure;
using AutoMapper;

namespace TCG.EMS
{
    // <-***** Microsoft (2026) [1] - START
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // <-***** Microsoft (2026) [1] - END

            //builder.Services.AddAuthorization();
            //builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
                    )
                );

            // 2/2/2026 https://www.youtube.com/watch?v=b7-BC7VyyLk v
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(GenericDbService<,>));
            builder.Services.AddScoped<LoginAuthService>();

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

            // <-***** Microsoft (2026) [1] - START

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.Run();

            // <-***** Microsoft (2026) [1] - END
        }
    }
}
//<-***** Microsoft (2026) [1] - END