using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TCG.Application.Services;

namespace TCG.Application.Interfaces;

public interface IUserFromCookies
{
    public Task<int?> GetUserIdAsync();
}