using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Services;
using TCG.Domain.Entities;

namespace TCG.Application.Services
{
    public class LoginService
    {
        private readonly LoginAuthService _loginAuthService;
        private readonly GenericDbService<Staff, StaffDto> _staffService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(
            LoginAuthService loginAuthService,
            GenericDbService<Staff, StaffDto> staffService,
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

            var role = user.StaffRoleHead ? "head"
                     : user.StaffRoleManagement ? "management"
                     : "staff";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.StaffId.ToString()),
                new Claim(ClaimTypes.Email, user.StaffEmail),
                new Claim(ClaimTypes.Role, role)
            };

            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, "default")
            );

            return LoginResult.SuccessfulAttempt(principal);
        }


        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("default");
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