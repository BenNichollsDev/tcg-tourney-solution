/*
Program: Local Games Store Management System
Filename: PlayerService.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Repositories;

public class PlayerService : IPlayerService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _u;
    private readonly IPasswordHasher<Player> _passwordHasher;

    public PlayerService(AppDbContext context, IMapper mapper, IUnitOfWork u, IPasswordHasher<Player> passwordHasher)
    {
        _context = context;
        _mapper = mapper;
        _u = u;
        _passwordHasher = passwordHasher;
    }

    public async Task<PlayerDto?> GetByIdAsync(int playerId)
    {
        var player = await _context.Set<Player>()
            .AsNoTracking()
            .Where(s => s.PlayerId == playerId)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return player ?? null;
    }

    public async Task<IEnumerable<PlayerDto>> GetAllAsync()
    {
        return await _context.Set<Player>()
            .AsNoTracking()
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<PlayerDto?> GetByAsync(Expression<Func<Player, bool>> predicate)
    {
        var player = await _context.Set<Player>()
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return player ?? null;
    }

    public async Task<PlayerDto> CreateAsync(PlayerDto playerDto)
    {
        await EmailIsUniqueAsync(playerDto.PlayerEmail);
        await PhoneIsUniqueAsync(playerDto.PlayerPhone);

        var player = _mapper.Map<Player>(playerDto);

        await _context.Set<Player>().AddAsync(player);
        await _u.SaveChangesAsync();

        return _mapper.Map<PlayerDto>(player);
    }

    public async Task<PlayerDto> UpdateAsync(PlayerDto playerDto)
    {
        var existingPlayer = await _context.Set<Player>()
            .FirstOrDefaultAsync(s => s.PlayerId == playerDto.PlayerId);

        if (existingPlayer == null)
            throw new Exception($"Player with id {playerDto.PlayerId} not found");

        await EmailIsUniqueAsync(playerDto.PlayerEmail, playerDto.PlayerId);
        await PhoneIsUniqueAsync(playerDto.PlayerPhone, playerDto.PlayerId);

        _mapper.Map(playerDto, existingPlayer);

        _context.Set<Player>().Update(existingPlayer);
        await _u.SaveChangesAsync();

        return _mapper.Map<PlayerDto>(existingPlayer);
    }

    public async Task<PlayerDto> DeleteAsync(int id)
    {
        var player = await _context.Set<Player>()
            .FirstOrDefaultAsync(s => s.PlayerId == id);

        if (player == null)
            throw new Exception($"Player with id {id} not found");

        _context.Set<Player>().Remove(player);
        await _u.SaveChangesAsync();

        return _mapper.Map<PlayerDto>(player);
    }

    public async Task EmailIsUniqueAsync(string email, int? currentPlayerId = null)
    {
        var exists = await _context.Set<Player>()
            .AnyAsync(s => s.PlayerEmail == email &&
                           (!currentPlayerId.HasValue || s.PlayerId != currentPlayerId));

        if (exists)
            throw new ValidationException("Email already exists.");
    }

    public async Task PhoneIsUniqueAsync(string phone, int? currentPlayerId = null)
    {
        var exists = await _context.Set<Player>()
            .AnyAsync(s => s.PlayerPhone == phone &&
                           (!currentPlayerId.HasValue || s.PlayerId != currentPlayerId));

        if (exists)
            throw new ValidationException("Phone number already exists.");
    }

    public async Task<bool> VerifyPasswordAsync(int playerId, string password)
    {
        var player = await _context.Set<Player>()
            .FirstOrDefaultAsync(p => p.PlayerId == playerId);

        if (player == null || string.IsNullOrWhiteSpace(player.PlayerPassword))
            return false;

        var result = _passwordHasher.VerifyHashedPassword(player, player.PlayerPassword, password);
        return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
    }

    public async Task<PlayerDto> CreateWithDefaultPasswordAsync(PlayerDto playerDto)
    {
        await EmailIsUniqueAsync(playerDto.PlayerEmail);
        await PhoneIsUniqueAsync(playerDto.PlayerPhone);

        var player = _mapper.Map<Player>(playerDto);

        // Hash the default password "123"
        const string defaultPassword = "123";
        player.PlayerPassword = _passwordHasher.HashPassword(player, defaultPassword);

        await _context.Set<Player>().AddAsync(player);
        await _u.SaveChangesAsync();

        return _mapper.Map<PlayerDto>(player);
    }
}

