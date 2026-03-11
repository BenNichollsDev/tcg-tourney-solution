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
}