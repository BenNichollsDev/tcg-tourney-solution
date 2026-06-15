//
// Program: Local Games Store Management System
// Filename: ITournamentScoringService.cs
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
using System.Text;
using TCG.Application.Dtos;

namespace TCG.Application.Interfaces.Services
{
    public interface ITournamentScoringService
    {
        public class PlayerComputedStats
        {
            public int TournamentPlayerId { get; set; }

            public List<int> OpponentIds { get; set; } = new List<int>();

            public string PlayerName { get; set; } = string.Empty;

            public int MatchesPlayed { get; set; }

            public int GamesPlayed { get; set; }

            public int Wins { get; set; }

            public int Draws { get; set; }

            public int Losses { get; set; }

            // mtg tiebreaker 1
            public int MatchPoints { get; set; } // wins = 3 + draws = 1

            public double MatchWinPercent { get; set; } // match wins / matches played * 100

            // mtg tiebreaker 2
            // pkmn tiebreaker 1
            public double OpMatchWinPercent { get; set; } // opponent match win percentage

            // mtg tiebreaker 3
            public double GameWinPercent { get; set; } // game wins / games played * 100

            // mtg tiebreaker 4
            public double OpGameWinPercent { get; set; } // opponent game win percentage

            // pkmn tiebreaker 2
            public double OpOpMatchWinPercent { get; set; } // opponent's opponent match win percentage

            // pkmn tiebreaker 3
            public int HeadToHeadPosition { get; set; }

            public int Position { get; set; } // Position ranking which is updated per round

            public bool IsDisqualified { get; set; }

            public bool IsDropped { get; set; }

            public int Byes { get; set; }
        }

        // Returns a dictionary consisting of player info and stats
        public Task<Dictionary<int, PlayerComputedStats>> ComputeTournamentStandings(
            int tournamentId
        );

        // Saves rankings
        public Task<bool> SavePositions(
            Dictionary<int, PlayerComputedStats> players
        );
    }
}


