using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Repositories;

public class TournamentPlayerService
(
    AppDbContext context,
    IMapper mapper,
    IUnitOfWork u
)
: ITournamentPlayerService
{
    public async Task<TournamentPlayerDto> GetByIdAsync(int tpId)
    {
        var tournamentPlayer = await context.TournamentPlayers
            .AsNoTracking()
            .Where(s => s.TpId == tpId)
            .ProjectTo<TournamentPlayerDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return tournamentPlayer ?? throw new Exception($"Player with id {tpId} not found");
    }

    public async Task<IEnumerable<TournamentPlayerDto>> GetAllAsync()
    {
        return await context.TournamentPlayers
            .AsNoTracking()
            .ProjectTo<TournamentPlayerDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TournamentPlayerDto> GetByAsync(Expression<Func<TournamentPlayer, bool>> predicate)
    {
        var tournamentPlayer = await context.TournamentPlayers
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<TournamentPlayerDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return tournamentPlayer ?? throw new Exception("Player not found");
    }

    public async Task<TournamentPlayerDto> CreateAsync(TournamentPlayerDto tournamentPlayerDto)
    {
        var tournamentPlayer = mapper.Map<TournamentPlayer>(tournamentPlayerDto);

        await context.TournamentPlayers.AddAsync(tournamentPlayer);
        await u.SaveChangesAsync();

        return mapper.Map<TournamentPlayerDto>(tournamentPlayer);
    }

    public async Task<TournamentPlayerDto> UpdateAsync(TournamentPlayerDto tournamentPlayerDto)
    {
        var existingTournamentPlayer = await context.TournamentPlayers
            .FirstOrDefaultAsync(s => s.TpId == tournamentPlayerDto.TpId);

        if (existingTournamentPlayer == null)
            throw new Exception($"Player with id {tournamentPlayerDto.TpId} not found");

        mapper.Map(tournamentPlayerDto, existingTournamentPlayer);

        context.TournamentPlayers.Update(existingTournamentPlayer);
        await u.SaveChangesAsync();

        return mapper.Map<TournamentPlayerDto>(existingTournamentPlayer);
    }

    public async Task<TournamentPlayerDto> DeleteAsync(int id)
    {
        var tournamentPlayer = await context.TournamentPlayers
            .FirstOrDefaultAsync(s => s.TpId == id);

        if (tournamentPlayer == null)
            throw new Exception($"Player with id {id} not found");

        context.TournamentPlayers.Remove(tournamentPlayer);
        await u.SaveChangesAsync();

        return mapper.Map<TournamentPlayerDto>(tournamentPlayer);
    }
}