using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TCG.Application.Interfaces.Services;

namespace TCG.Application.Services;

public class UserFromCookies : IUserFromCookies
{
    private readonly AuthenticationStateProvider _authProvider;

    public UserFromCookies(AuthenticationStateProvider authProvider)
    {
        _authProvider = authProvider;
    }

    public async Task<string?> GetRoleAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
            return null;

        return user.FindFirst(ClaimTypes.Role)?.Value;
    }

    public async Task<int?> GetUserIdAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
            return null;

        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return id != null ? int.Parse(id) : null;
    }
}