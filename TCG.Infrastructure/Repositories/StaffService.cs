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
    IUnitOfWork u
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

    public async Task<StaffDto> CreateAsync(StaffDto staffDto)
    {
        
        await EmailIsUniqueAsync(staffDto.StaffEmail);
        await PhoneIsUniqueAsync(staffDto.StaffMobile);
        
        var staff = mapper.Map<Staff>(staffDto);

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
}