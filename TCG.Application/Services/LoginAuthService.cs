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
        }

        public async Task<bool> VerifyLoginAsync(string email, string password)
        {
            var staff = await _staffService.GetByAsync(e => e.StaffEmail == email);

            if (staff == null)
                return false;

            var isValid = await _staffService.VerifyPasswordAsync(staff.StaffId, password);

            return isValid;
        }
    }
}
