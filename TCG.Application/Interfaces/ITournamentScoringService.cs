using System;
using System.Collections.Generic;
using System.Text;
using TCG.Application.Dtos;
using TCG.Application.Interfaces.Services;

namespace TCG.Application.Interfaces
{
    public interface ITournamentScoringService
    {
        public class PlayerComputedStats
        {
            public int TournamentPlayerId { get; set; }
            public string PlayerName { get; set; } = string.Empty;
            public int Wins { get; set; }
            public int Draws { get; set; }
            public int Losses { get; set; }
            public int Points { get; set; } // wins = 3 + draws = 1
            public double OpWinPercent { get; set; } // Opponent Win Percentage
            public double GameWinPercent { get; set; }
            public double OpGameWinPercent { get; set; } // MTG only: Opponent's Game Win Percentage
            public int GamesWon { get; set; }
            public int GamesPlayed { get; set; }
            public int Position { get; set; } // Final ranking position in tournament
            public bool IsDisqualified { get; set; }
            public bool IsDropped { get; set; }
        }

        // When the whole program is done, do the commented functions

        // Returns a dictionary consisting of player info and stats
        // public Dictionary<int, PlayerComputedStats> ComputeTournamentStandings(
        public void ComputeTournamentStandings(
            int tournamentId
        );

        // Organizes the computed stats for display for MTG tournaments
        // and does tiebreakers
        // public Dictionary<int, PlayerComputedStats> OrganizeMtgStatsForDisplay(
        public void OrganizeMtgStatsForDisplay(
            int tournamentId,
            Dictionary<int, PlayerComputedStats> standings
        );

        // Organizes the computed stats for display for PKMN tournaments
        // and does tiebreakers
        // public Dictionary<int, PlayerComputedStats> OrganizePkmnStatsForDisplay(
        public void OrganizePkmnStatsForDisplay(
            int tournamentId,
            Dictionary<int, PlayerComputedStats> standings
        );


        // Saves rankings
        // public bool SavePositions(
        public void SavePositions(
            Dictionary<int, PlayerComputedStats> players
        );


        // Will probably need seperate mtg and pkmn tiebreakers
    }
}
