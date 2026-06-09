using System.Linq.Expressions;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces.Services;

public interface IPairingService
{
    Task<PairingDto> GetByIdAsync(int leagueId);

    Task<IEnumerable<PairingDto>> GetAllAsync();

    Task<IEnumerable<PairingDto>> GetAllWhereAsync(Expression<Func<Pairing, bool>> predicate);

    Task<PairingDto> GetByAsync(Expression<Func<Pairing, bool>> predicate);

    Task<PairingDto> CreateAsync(PairingDto leagueDto);

    Task<PairingDto> UpdateAsync(PairingDto leagueDto);

    Task<PairingDto> DeleteAsync(int id);
}