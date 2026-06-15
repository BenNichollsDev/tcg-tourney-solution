//
// Program: Local Games Store Management System
// Filename: PairingService.cs
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

public class PairingService
(
    AppDbContext context,
    IMapper mapper,
    IUnitOfWork u
)
: IPairingService
{
    public async Task<PairingDto> GetByIdAsync(int leagueId)
    {
        var league = await context.Pairings
            .AsNoTracking()
            .Where(s => s.PairingId == leagueId)
            .ProjectTo<PairingDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return league ?? throw new Exception($"Pairing with id {leagueId} not found");
    }

    public async Task<IEnumerable<PairingDto>> GetAllAsync()
    {
        return await context.Pairings
            .AsNoTracking()
            .ProjectTo<PairingDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<PairingDto>> GetAllWhereAsync(Expression<Func<Pairing, bool>> predicate)
    {
        return await context.Pairings
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<PairingDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<PairingDto> GetByAsync(Expression<Func<Pairing, bool>> predicate)
    {
        var league = await context.Pairings
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<PairingDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return league ?? throw new Exception("Pairing not found");
    }

    public async Task<PairingDto> CreateAsync(PairingDto leagueDto)
    {
        var league = mapper.Map<Pairing>(leagueDto);

        await context.Pairings.AddAsync(league);
        await u.SaveChangesAsync();

        return mapper.Map<PairingDto>(league);
    }

    public async Task<PairingDto> UpdateAsync(PairingDto leagueDto)
    {
        var existingPairing = await context.Pairings
            .FirstOrDefaultAsync(s => s.PairingId == leagueDto.PairingId);

        if (existingPairing == null)
            throw new Exception($"Pairing with id {leagueDto.PairingId} not found");

        mapper.Map(leagueDto, existingPairing);

        context.Pairings.Update(existingPairing);
        await u.SaveChangesAsync();

        return mapper.Map<PairingDto>(existingPairing);
    }

    public async Task<PairingDto> DeleteAsync(int id)
    {
        var league = await context.Pairings
            .FirstOrDefaultAsync(s => s.PairingId == id);

        if (league == null)
            throw new Exception($"Pairing with id {id} not found");

        context.Pairings.Remove(league);
        await u.SaveChangesAsync();

        return mapper.Map<PairingDto>(league);
    }
}

