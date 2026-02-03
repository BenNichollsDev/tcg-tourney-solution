using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using TCG.Application.Dtos;
using TCG.Application.Services;
using TCG.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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

            foreach (var staff in staffList)
            {
                var hashed = _hasher.HashPassword(null, staff.StaffPassword);
                staff.StaffPassword = hashed;
                await _staffService.UpdateAsync(staff);
            }

            var login = await _staffService.GetByAsync(e => e.StaffEmail == email);

            if (login == null)
                return false;

            var loginResult = _hasher.VerifyHashedPassword(null, login.StaffPassword!, password.ToString());

            return loginResult != PasswordVerificationResult.Failed;
        }
    }
}
