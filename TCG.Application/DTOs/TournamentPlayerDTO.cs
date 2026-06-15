//
// Program: Local Games Store Management System
// Filename: TournamentPlayerDTO.cs
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

namespace TCG.Application.Dtos
{
    public class TournamentPlayerDto
    {
        public int TournamentPlayerId { get; set; }

        public int TournamentId { get; set; }

        public int PlayerId { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        // Round-robin fields
        public int? PlayerRoundRobinWins { get; set; }

        public int? PlayerRoundRobinDraws { get; set; }

        public int? PlayerRoundRobinLosses { get; set; }

        public int? PlayerRoundRobinScore { get; set; }

        public int? PlayerRoundRobinMatchPoints { get; set; }

        public int? PlayerRoundRobinPoints { get; set; }

        // Swiss fields
        public int? PlayerSwissWins { get; set; }

        public int? PlayerSwissDraws { get; set; }

        public int? PlayerSwissLosses { get; set; }

        public int? PlayerSwissScore { get; set; }

        public int? PlayerSwissMatchPoints { get; set; }

        public int? PlayerSwissPoints { get; set; }

        // Tracking for byes and games
        public int? PlayerBye { get; set; }

        public int? GamesPlayed { get; set; }

        public int? MatchesPlayed { get; set; }

        // Player status tracking
        public bool TpDisqualified { get; set; }

        public bool TpDropped { get; set; }

        public int? TpPosition { get; set; }
    }
}

