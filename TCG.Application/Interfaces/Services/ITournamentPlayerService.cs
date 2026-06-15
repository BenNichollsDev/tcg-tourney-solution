//
// Program: Local Games Store Management System
// Filename: ITournamentPlayerService.cs
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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces.Services
{
    public interface ITournamentPlayerService
    {
        Task<TournamentPlayerDto> GetByIdAsync(int tpId);

        Task<IEnumerable<TournamentPlayerDto>> GetAllAsync();

        Task<IEnumerable<TournamentPlayerDto>> GetAllWhereAsync(Expression<Func<TournamentPlayer, bool>> predicate);
        
        Task<TournamentPlayerDto> GetByAsync(Expression<Func<TournamentPlayer, bool>> predicate);
        
        Task<TournamentPlayerDto> CreateAsync(TournamentPlayerDto tournamentPlayerDto);
        
        Task<TournamentPlayerDto> UpdateAsync(TournamentPlayerDto tournamentPlayerDto);
        
        Task<TournamentPlayerDto> DeleteAsync(int id);
    }
}

