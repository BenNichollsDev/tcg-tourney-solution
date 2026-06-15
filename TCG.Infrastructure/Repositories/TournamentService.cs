//
// Program: Local Games Store Management System
// Filename: TournamentService.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 14/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Repositories;

public class TournamentService
(
    AppDbContext context,
    IMapper mapper,
    IUnitOfWork u
)
: ITournamentService
{
    public async Task<TournamentDto> GetByIdAsync(int tournamentId)
    {
        var tournament = await context.Tournaments
            .AsNoTracking()
            .Where(s => s.TournamentId == tournamentId)
            .ProjectTo<TournamentDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return tournament ?? throw new Exception($"Tournament with id {tournamentId} not found");
    }

    public async Task<IEnumerable<TournamentDto>> GetAllAsync()
    {
        return await context.Tournaments
            .AsNoTracking()
            .ProjectTo<TournamentDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TournamentDto> GetByAsync(Expression<Func<Tournament, bool>> predicate)
    {
        var tournament = await context.Tournaments
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<TournamentDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return tournament ?? throw new Exception("Tournament not found");
    }

    public async Task<TournamentDto> CreateAsync(TournamentDto tournamentDto)
    {
        var tournament = mapper.Map<Tournament>(tournamentDto);

        await context.Tournaments.AddAsync(tournament);
        await u.SaveChangesAsync();

        return mapper.Map<TournamentDto>(tournament);
    }

    public async Task<TournamentDto> UpdateAsync(TournamentDto tournamentDto)
    {
        var existingTournament = await context.Tournaments
            .FirstOrDefaultAsync(s => s.TournamentId == tournamentDto.TournamentId);

        if (existingTournament == null)
            throw new Exception($"Tournament with id {tournamentDto.TournamentId} not found");

        mapper.Map(tournamentDto, existingTournament);

        context.Tournaments.Update(existingTournament);
        await u.SaveChangesAsync();

        return mapper.Map<TournamentDto>(existingTournament);
    }

    public async Task<TournamentDto> DeleteAsync(int id)
    {
        var tournament = await context.Tournaments
            .FirstOrDefaultAsync(s => s.TournamentId == id);

        if (tournament == null)
            throw new Exception($"Tournament with id {id} not found");

        context.Tournaments.Remove(tournament);
        await u.SaveChangesAsync();

        return mapper.Map<TournamentDto>(tournament);
    }
}

