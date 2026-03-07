using System.Linq.Expressions;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces.Services;

public interface ILeagueService
{
    Task<LeagueDto> GetByIdAsync(int leagueId);

    Task<IEnumerable<LeagueDto>> GetAllAsync();

    Task<LeagueDto> GetByAsync(Expression<Func<League, bool>> predicate);

    Task<LeagueDto> CreateAsync(LeagueDto leagueDto);

    Task<LeagueDto> UpdateAsync(LeagueDto leagueDto);

    Task<LeagueDto> DeleteAsync(int id);
}