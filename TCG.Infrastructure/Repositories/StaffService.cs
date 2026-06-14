/*
Program: Local Games Store Management System
Filename: StaffService.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Repositories;

public class StaffService
(
    AppDbContext context,
    IMapper mapper,
    IUnitOfWork u,
    Microsoft.AspNetCore.Identity.IPasswordHasher<TCG.Domain.Entities.Staff> hasher
)
: IStaffService
{
    public async Task<StaffDto?> GetByIdAsync(int staffId)
    {
        var staff = await context.Staff
            .AsNoTracking()
            .Where(s => s.StaffId == staffId)
            .ProjectTo<StaffDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return staff ?? null;
    }

    public async Task<IEnumerable<StaffDto>> GetAllAsync()
    {
        return await context.Staff
            .AsNoTracking()
            .ProjectTo<StaffDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<StaffDto?> GetByAsync(Expression<Func<Staff, bool>> predicate)
    {
        var staff = await context.Staff
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<StaffDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return staff ?? null;
    }

    public async Task<bool> VerifyPasswordAsync(int staffId, string password)
    {
        var staff = await context.Staff.FirstOrDefaultAsync(s => s.StaffId == staffId);
        if (staff == null || string.IsNullOrWhiteSpace(staff.StaffPassword))
            return false;

        var result = hasher.VerifyHashedPassword(staff, staff.StaffPassword, password);
        return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success || result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.SuccessRehashNeeded;
    }

    public async Task<StaffDto> CreateAsync(StaffDto staffDto)
    {
        await EmailIsUniqueAsync(staffDto.StaffEmail);
        await PhoneIsUniqueAsync(staffDto.StaffMobile);

var staff = mapper.Map<Staff>(staffDto);

        // Hash password before saving
        if (!string.IsNullOrEmpty(staffDto.StaffPassword))
        {
            staff.StaffPassword = hasher.HashPassword(staff, staffDto.StaffPassword);
        }

        await context.Staff.AddAsync(staff);
        await u.SaveChangesAsync();

        return mapper.Map<StaffDto>(staff);
    }

    public async Task<StaffDto> UpdateAsync(StaffDto staffDto)
    {
        var existingStaff = await context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == staffDto.StaffId);

        if (existingStaff == null)
            throw new Exception($"Staff with id {staffDto.StaffId} not found");

        
        await EmailIsUniqueAsync(staffDto.StaffEmail, staffDto.StaffId);
        await PhoneIsUniqueAsync(staffDto.StaffMobile, staffDto.StaffId);

        // If new password provided, hash and set it. otherwise keep existing password
        if (!string.IsNullOrEmpty(staffDto.StaffPassword))
        {
            existingStaff.StaffPassword = hasher.HashPassword(existingStaff, staffDto.StaffPassword);
        }

        mapper.Map(staffDto, existingStaff);

        context.Staff.Update(existingStaff);
        await u.SaveChangesAsync();

        return mapper.Map<StaffDto>(existingStaff);
    }

    public async Task<StaffDto> DeleteAsync(int id)
    {
        var staff = await context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            throw new Exception($"Staff with id {id} not found");

        context.Staff.Remove(staff);
        await u.SaveChangesAsync();

        return mapper.Map<StaffDto>(staff);
    }
    
    public async Task EmailIsUniqueAsync(string email, int? currentStaffId = null)
    {
        var exists = await context.Staff
            .AnyAsync(s => s.StaffEmail == email && 
                           (!currentStaffId.HasValue || s.StaffId != currentStaffId));

        if (exists)
            throw new ValidationException("Email already exists.");
    }
    
    public async Task PhoneIsUniqueAsync(string phone, int? currentStaffId = null)
    {
        var exists = await context.Staff
            .AnyAsync(s => s.StaffMobile == phone && 
                           (!currentStaffId.HasValue || s.StaffId != currentStaffId));

        if (exists)
            throw new ValidationException("Phone number already exists.");
    }

    public async Task<StaffDto> CreateWithDefaultPasswordAsync(StaffDto staffDto)
    {
        await EmailIsUniqueAsync(staffDto.StaffEmail);
        await PhoneIsUniqueAsync(staffDto.StaffMobile);

        var staff = mapper.Map<Staff>(staffDto);

        // Hash the default password "123"
        const string defaultPassword = "123";
        staff.StaffPassword = hasher.HashPassword(staff, defaultPassword);

        await context.Staff.AddAsync(staff);
        await u.SaveChangesAsync();

        return mapper.Map<StaffDto>(staff);
    }
}
