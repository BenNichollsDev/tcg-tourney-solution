using System.Linq.Expressions;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces;

public interface ITournamentService
{
    Task<TournamentDto> GetByIdAsync(int tournamentId);

    Task<IEnumerable<TournamentDto>> GetAllAsync();

    Task<TournamentDto> GetByAsync(Expression<Func<Tournament, bool>> predicate);

    Task<TournamentDto> CreateAsync(TournamentDto tournamentDto);

    Task<TournamentDto> UpdateAsync(TournamentDto tournamentDto);

    Task<TournamentDto> DeleteAsync(int id);
}