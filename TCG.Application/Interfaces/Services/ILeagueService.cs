//
// Program: Local Games Store Management System
// Filename: ILeagueService.cs
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

public interface ILeagueService
{
    Task<LeagueDto> GetByIdAsync(int leagueId);

    Task<IEnumerable<LeagueDto>> GetAllAsync();

    Task<LeagueDto> GetByAsync(Expression<Func<League, bool>> predicate);

    Task<LeagueDto> CreateAsync(LeagueDto leagueDto);

    Task<LeagueDto> UpdateAsync(LeagueDto leagueDto);

    Task<LeagueDto> DeleteAsync(int id);
}

