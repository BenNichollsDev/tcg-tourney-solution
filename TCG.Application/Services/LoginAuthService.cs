using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using TCG.Application.Dtos;
using TCG.Application.Services;
using TCG.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TCG.Application.Services
{
    public class LoginAuthService
    {
        private readonly GenericDbService<Staff, StaffDto> _staffService;
        private readonly PasswordHasher<object> _hasher;

        public LoginAuthService(GenericDbService<Staff, StaffDto> staffService)
        {
            _staffService = staffService;
            _hasher = new PasswordHasher<object>();
        }

        public async Task<bool> VerifyLoginAsync(string email, string password)
        {
            var staffList = await _staffService.GetAllAsync();

            // FIX The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.

            var login = await _staffService.GetByAsync(e => e.StaffEmail == email);

            if (login == null)
                return false;

            var loginResult = _hasher.VerifyHashedPassword(null, login.StaffPassword!, password.ToString());

            return loginResult != PasswordVerificationResult.Failed;
        }
    }
}
