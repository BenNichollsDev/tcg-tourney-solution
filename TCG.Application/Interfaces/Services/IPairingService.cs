/*
Program: Local Games Store Management System
Filename: IPairingService.cs
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
