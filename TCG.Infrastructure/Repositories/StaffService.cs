using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
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
    public async Task<StaffDto> GetByIdAsync(int staffId)
    {
        var staff = await context.Staff
            .AsNoTracking()
            .Where(s => s.StaffId == staffId)
            .ProjectTo<StaffDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return staff ?? throw new Exception($"Staff with id {staffId} not found");
    }

    public async Task<IEnumerable<StaffDto>> GetAllAsync()
    {
        return await context.Staff
            .AsNoTracking()
            .ProjectTo<StaffDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<StaffDto> GetByAsync(Expression<Func<Staff, bool>> predicate)
    {
        var staff = await context.Staff
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<StaffDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return staff ?? throw new Exception("Staff not found");
    }

    public async Task<StaffDto> CreateAsync(StaffDto staffDto)
    {
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
}