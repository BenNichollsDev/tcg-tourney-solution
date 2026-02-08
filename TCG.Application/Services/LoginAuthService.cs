using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Services;
using TCG.Domain.Entities;

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
            // REMINDER TO INCORPORATE SALT IN FUTURE
            // MANUALLY UPDATE PASSWORDS TO USE 123 FOR NOW

            var login = await _staffService.GetByAsync(e => e.StaffEmail == email);

            if (login == null)
                return false;

            if (login.StaffPassword! != password)
                return false;

            return true;
        }
    }
}
