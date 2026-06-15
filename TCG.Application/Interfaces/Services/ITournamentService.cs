//
// Program: Local Games Store Management System
// Filename: ITournamentService.cs
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
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces.Services;

public interface ITournamentService
{
    Task<TournamentDto> GetByIdAsync(int tournamentId);

    Task<IEnumerable<TournamentDto>> GetAllAsync();

    Task<TournamentDto> GetByAsync(Expression<Func<Tournament, bool>> predicate);

    Task<TournamentDto> CreateAsync(TournamentDto tournamentDto);

    Task<TournamentDto> UpdateAsync(TournamentDto tournamentDto);

    Task<TournamentDto> DeleteAsync(int id);
}

