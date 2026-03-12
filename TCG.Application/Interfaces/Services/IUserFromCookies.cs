using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TCG.Application.Services;

namespace TCG.Application.Interfaces.Services;

public interface IUserFromCookies
{
    public Task<string?> GetRoleAsync();

    public Task<int?> GetUserIdAsync();
}