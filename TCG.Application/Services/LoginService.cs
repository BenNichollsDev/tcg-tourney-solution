using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Application.Services;
using TCG.Domain.Entities;

namespace TCG.Application.Services
{
    public class LoginService
    {
        private readonly LoginAuthService _loginAuthService;
        private readonly IStaffService _staffService;
        private readonly IPlayerService _playerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(
            LoginAuthService loginAuthService,
            IStaffService staffService,
            IPlayerService playerService,
            IHttpContextAccessor httpContextAccessor)
        {
            _loginAuthService = loginAuthService;
            _staffService = staffService;
            _playerService = playerService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResult> LoginAsync(string email, string password, bool isStaff)
        {
            var claims = new List<Claim>();

            if (isStaff)
            {
                if (!await _loginAuthService.VerifyLoginAsync(email, password, isStaff))
                    return LoginResult.FailedAttempt();

                var user = await _staffService.GetByAsync(e => e.StaffEmail == email);

                if (user is null)
                    return LoginResult.FailedAttempt();

                claims.AddRange([
                    new Claim(ClaimTypes.NameIdentifier, user.StaffId.ToString()),
                    new Claim(ClaimTypes.Email, user.StaffEmail),
                    new Claim("StaffFirstName", user.StaffFirstName ?? string.Empty),
                    new Claim("StaffSurname", user.StaffSurname ?? string.Empty)
                ]);
            }
            else
            {
                if (!await _loginAuthService.VerifyLoginAsync(email, password, isStaff))
                    return LoginResult.FailedAttempt();

                var user = await _playerService.GetByAsync(e => e.PlayerEmail == email);

                if (user is null)
                    return LoginResult.FailedAttempt();

                claims.AddRange([
                    new Claim(ClaimTypes.NameIdentifier, user.PlayerId.ToString()),
                    new Claim(ClaimTypes.Email, user.PlayerEmail),
                    new Claim("StaffFirstName", user.PlayerFirstName ?? string.Empty),
                    new Claim("StaffSurname", user.PlayerLastName ?? string.Empty)
                ]);
            }

            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
            );

            return LoginResult.SuccessfulAttempt(principal);
        }


        public async Task LogoutAsync()
        {
            var accessor = _httpContextAccessor.HttpContext;
            if (accessor is not null)
            {
                await accessor.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }

    public class LoginResult
    {
        public bool WasSuccess { get; }

        public ClaimsPrincipal? CPrinciple { get; }

        private LoginResult(bool success, ClaimsPrincipal? principal = null)
        {
            WasSuccess = success;
            CPrinciple = principal;
        }

        public static LoginResult FailedAttempt() => new(false, null);
        public static LoginResult SuccessfulAttempt(ClaimsPrincipal principal) => new(true, principal);
    }
}