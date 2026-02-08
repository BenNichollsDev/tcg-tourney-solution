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

        public LoginService
            (LoginAuthService loginAuthService,
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
            {
                return LoginResult.FailedAttempt("Invalid email or password.");
            }

            StaffDto user = await _staffService.GetByAsync(e => e.StaffEmail == email);

            string userRole = user.StaffRoleHead ? "head" :
                              user.StaffRoleManagement ? "management" : "staff";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.StaffId.ToString()),
                new Claim(ClaimTypes.Email, user.StaffEmail),
                new Claim(ClaimTypes.Role, userRole)
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "default"));

            await _httpContextAccessor.HttpContext.SignInAsync("default", principal);

            return LoginResult.SuccessfulAttempt(principal);
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("default");
        }
    }

    public class LoginResult
    {
        public bool wasSuccess { get; }

        public ClaimsPrincipal claimsPrincipal { get; }

        public string errorMessage { get; }

        private LoginResult(bool success, ClaimsPrincipal? principal = null, string? error = null)
        {
            wasSuccess = success;
            claimsPrincipal = principal;
            errorMessage = error;
        }

        public static LoginResult FailedAttempt(string error) => new(false, null, error);
        public static LoginResult SuccessfulAttempt(ClaimsPrincipal principal) => new(true, principal);
    }
}