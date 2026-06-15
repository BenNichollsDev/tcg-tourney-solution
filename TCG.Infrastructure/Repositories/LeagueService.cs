//
// Program: Local Games Store Management System
// Filename: LeagueService.cs
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

public class LeagueService
(
    AppDbContext context,
    IMapper mapper,
    IUnitOfWork u
)
: ILeagueService
{
    public async Task<LeagueDto> GetByIdAsync(int leagueId)
    {
        var league = await context.Leagues
            .AsNoTracking()
            .Where(s => s.LeagueId == leagueId)
            .ProjectTo<LeagueDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return league ?? throw new Exception($"League with id {leagueId} not found");
    }

    public async Task<IEnumerable<LeagueDto>> GetAllAsync()
    {
        return await context.Leagues
            .AsNoTracking()
            .ProjectTo<LeagueDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<LeagueDto> GetByAsync(Expression<Func<League, bool>> predicate)
    {
        var league = await context.Leagues
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<LeagueDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return league ?? throw new Exception("League not found");
    }

    public async Task<LeagueDto> CreateAsync(LeagueDto leagueDto)
    {
        var league = mapper.Map<League>(leagueDto);

        await context.Leagues.AddAsync(league);
        await u.SaveChangesAsync();

        return mapper.Map<LeagueDto>(league);
    }

    public async Task<LeagueDto> UpdateAsync(LeagueDto leagueDto)
    {
        var existingLeague = await context.Leagues
            .FirstOrDefaultAsync(s => s.LeagueId == leagueDto.LeagueId);

        if (existingLeague == null)
            throw new Exception($"League with id {leagueDto.LeagueId} not found");

        mapper.Map(leagueDto, existingLeague);

        context.Leagues.Update(existingLeague);
        await u.SaveChangesAsync();

        return mapper.Map<LeagueDto>(existingLeague);
    }

    public async Task<LeagueDto> DeleteAsync(int id)
    {
        var league = await context.Leagues
            .FirstOrDefaultAsync(s => s.LeagueId == id);

        if (league == null)
            throw new Exception($"League with id {id} not found");

        context.Leagues.Remove(league);
        await u.SaveChangesAsync();

        return mapper.Map<LeagueDto>(league);
    }
}

