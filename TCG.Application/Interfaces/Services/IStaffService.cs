/*
Program: Local Games Store Management System
Filename: IStaffService.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System.Linq.Expressions;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces.Services;

public interface IStaffService
{
    Task<StaffDto?> GetByIdAsync(int staffId);

    Task<IEnumerable<StaffDto>> GetAllAsync();

    Task<StaffDto?> GetByAsync(Expression<Func<Staff, bool>> predicate);

    Task<StaffDto> CreateAsync(StaffDto staffDto);

    Task<StaffDto> UpdateAsync(StaffDto staffDto);

    Task<StaffDto> DeleteAsync(int id);

    Task EmailIsUniqueAsync(string email, int? currentStaffId = null);

    Task PhoneIsUniqueAsync(string phone, int? currentStaffId = null);

    Task<bool> VerifyPasswordAsync(int staffId, string password);

    Task<StaffDto> CreateWithDefaultPasswordAsync(StaffDto staffDto);
}
