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
        private readonly IPlayerService _playerService;

        public LoginAuthService(IStaffService staffService, IPlayerService playerService)
        {
            _staffService = staffService;
            _playerService = playerService;
        }

        public async Task<bool> VerifyLoginAsync(string email, string password, bool isStaff)
        {
            if (isStaff)
            {
                var staff = await _staffService.GetByAsync(e => e.StaffEmail == email);

                if (staff == null)
                    return false;

                var isValid = await _staffService.VerifyPasswordAsync(staff.StaffId, password);

                return isValid;
            }
            else
            {
                var player = await _playerService.GetByAsync(e => e.PlayerEmail == email);

                if (player == null)
                    return false;

                var isValid = await _playerService.VerifyPasswordAsync(player.PlayerId, password);

                return isValid;
            }
        }
    }
}
