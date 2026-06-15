using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Application.Services;
using TCG.Domain.Entities;

namespace TCG.Application.Services
{
    public class LoginAuthService
    {
        private readonly IStaffService _staffService;

        public LoginAuthService(IStaffService staffService)
        {
            _staffService = staffService;
            //_hasher = new PasswordHasher<object>();
        }

        public async Task<bool> VerifyLoginAsync(string email, string password)
        {
            // IImplement the verifying login password from staffservice instead

            var login = await _staffService.GetByAsync(e => e.StaffEmail == email);

            if (login == null)
                return false;

            if (login.StaffPassword! != password)
                return false;

            return true;
        }
    }
}
