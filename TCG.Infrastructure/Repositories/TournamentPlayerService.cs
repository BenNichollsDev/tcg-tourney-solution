/*
Program: Local Games Store Management System
Filename: TournamentPlayerService.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Repositories;

public class TournamentPlayerService : ITournamentPlayerService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _u;

    public TournamentPlayerService(AppDbContext context, IMapper mapper, IUnitOfWork u)
    {
        _context = context;
        _mapper = mapper;
        _u = u;
    }

    public async Task<TournamentPlayerDto> GetByIdAsync(int tpId)
    {
        var tournamentPlayer = await _context.TournamentPlayers
            .AsNoTracking()
            .Where(s => s.TournamentPlayerId == tpId)
            .ProjectTo<TournamentPlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return tournamentPlayer ?? throw new Exception($"Player with id {tpId} not found");
    }

    public async Task<IEnumerable<TournamentPlayerDto>> GetAllAsync()
    {
        return await _context.TournamentPlayers
            .AsNoTracking()
            .ProjectTo<TournamentPlayerDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<TournamentPlayerDto>> GetAllWhereAsync(Expression<Func<TournamentPlayer, bool>> predicate)
    {
        return await _context.TournamentPlayers
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<TournamentPlayerDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TournamentPlayerDto> GetByAsync(Expression<Func<TournamentPlayer, bool>> predicate)
    {
        var tournamentPlayer = await _context.TournamentPlayers
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<TournamentPlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return tournamentPlayer ?? throw new Exception("Player not found");
    }

    public async Task<TournamentPlayerDto> CreateAsync(TournamentPlayerDto tournamentPlayerDto)
    {
        var tournamentPlayer = _mapper.Map<TournamentPlayer>(tournamentPlayerDto);

        await _context.TournamentPlayers.AddAsync(tournamentPlayer);
        await _u.SaveChangesAsync();

        return _mapper.Map<TournamentPlayerDto>(tournamentPlayer);
    }

    public async Task<TournamentPlayerDto> UpdateAsync(TournamentPlayerDto tournamentPlayerDto)
    {
        var existingTournamentPlayer = await _context.TournamentPlayers
            .FirstOrDefaultAsync(s => s.TournamentPlayerId == tournamentPlayerDto.TournamentPlayerId);

        if (existingTournamentPlayer == null)
            throw new Exception($"Player with id {tournamentPlayerDto.TournamentPlayerId} not found");

        _mapper.Map(tournamentPlayerDto, existingTournamentPlayer);

        _context.TournamentPlayers.Update(existingTournamentPlayer);
        await _u.SaveChangesAsync();

        return _mapper.Map<TournamentPlayerDto>(existingTournamentPlayer);
    }

    public async Task<TournamentPlayerDto> DeleteAsync(int id)
    {
        var tournamentPlayer = await _context.TournamentPlayers
            .FirstOrDefaultAsync(s => s.TournamentPlayerId == id);

        if (tournamentPlayer == null)
            throw new Exception($"Player with id {id} not found");

        _context.TournamentPlayers.Remove(tournamentPlayer);
        await _u.SaveChangesAsync();

        return _mapper.Map<TournamentPlayerDto>(tournamentPlayer);
    }
}

