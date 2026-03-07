using Microsoft.AspNetCore.Authentication;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(
            LoginAuthService loginAuthService,
            IStaffService staffService,
            IHttpContextAccessor httpContextAccessor)
        {
            _loginAuthService = loginAuthService;
            _staffService = staffService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            if (!await _loginAuthService.VerifyLoginAsync(email, password))
                return LoginResult.FailedAttempt();

            var user = await _staffService.GetByAsync(e => e.StaffEmail == email);

            if (user is null)
                return LoginResult.FailedAttempt();

            var role = user.StaffRoleHead ? "head"
                     : user.StaffRoleManagement ? "management"
                     : "staff";

            var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.StaffId.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.StaffEmail),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role)
            };

            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, "default")
            );

            return LoginResult.SuccessfulAttempt(principal);
        }


        public async Task LogoutAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx is not null)
            {
                await ctx.SignOutAsync("default");
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