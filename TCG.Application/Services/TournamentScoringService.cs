//
// Program: Local Games Store Management System
// Filename: TournamentScoringService.cs
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
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;
using TCG.Domain.Entities;

namespace TCG.Application.Services
{
    public class TournamentScoringService
         : ITournamentScoringService
    {
        private readonly ITournamentService _tService;
        private readonly ITournamentPlayerService _tpService;
        private readonly IPairingService _pService;

        private readonly Random _rnd;

        public TournamentScoringService(
            ITournamentService tService,
            ITournamentPlayerService tpService,
            IPairingService pService)
        {
            _tService = tService;
            _tpService = tpService;
            _pService = pService;
            _rnd = new Random();
        }

        public class PlayerComputedStats : ITournamentScoringService.PlayerComputedStats
        {
        }

        public async Task<Dictionary<int, ITournamentScoringService.PlayerComputedStats>> ComputeTournamentStandings(int tournamentId)
        {
            // Load tournament players from async service
            var tournamentPlayers = await _tpService.GetAllWhereAsync(tp => tp.TournamentId == tournamentId);

            var allPairings = await _pService.GetAllWhereAsync(p => p.TournamentId == tournamentId);

            Console.WriteLine($"Tournament ID = {tournamentId}");
            Console.WriteLine($"Pairings Count = {allPairings.Count()}");

            // Get full tournament to access finished status and max rounds
            var tournament = await _tService.GetByIdAsync(tournamentId);
            var tournamentGame = tournament.TournamentGame;
            var tournamentFormat = tournament.TournamentFormat;
            var tournamentIsFinished = tournament.TournamentFinished;
            var maxRounds = tournament.TournamentMaxRoundNum ?? 0;

            // Create a single standings dictionary with all players
            var standings = new Dictionary<int, ITournamentScoringService.PlayerComputedStats>();


            foreach (var player in tournamentPlayers)
            {
                // Initialise stats from DB: wins, draws, losses, opponents, etc.
                int playerGamesPlayed,
                    playerMatchesPlayed,
                    playerGameWins,
                    playerGameDraws,
                    playerGameLosses,
                    playerMatchWins,
                    playerMatchDraws,
                    playerMatchLosses,
                    playerMatchPoints,
                    playerGamePoints,
                    playerTotalGames;

                int byeCount = player.PlayerBye ?? 0;

                //byeCount = relevantPairings.Count(p => p.Player2Id == null
                //    && p.Player1Id == player.TournamentPlayerId);

                if (tournamentFormat == "RoundRobin")
                {
                    // Game-level statistics
                    playerGameWins = player.PlayerRoundRobinGameWins ?? 0;
                    playerGameDraws = player.PlayerRoundRobinGameDraws ?? 0;
                    playerGameLosses = player.PlayerRoundRobinGameLosses ?? 0;

                    // Match-level statistics
                    playerMatchWins = player.PlayerRoundRobinWins ?? 0;
                    playerMatchDraws = player.PlayerRoundRobinDraws ?? 0;
                    playerMatchLosses = player.PlayerRoundRobinLosses ?? 0;

                    // Derived match statistics
                    playerMatchPoints = player.PlayerRoundRobinMatchPoints ?? ((playerMatchWins * 3) + playerMatchDraws);
                    playerMatchesPlayed = playerMatchWins + playerMatchDraws + playerMatchLosses;

                    // Game-level tracking
                    playerGamesPlayed = player.GamesPlayed ?? (playerGameWins + playerGameDraws + playerGameLosses);
                    playerGamePoints = (playerGameWins * 3) + playerGameDraws;
                }
                else
                {
                    // Game-level statistics
                    playerGameWins = player.PlayerSwissGameWins ?? 0;
                    playerGameDraws = player.PlayerSwissGameDraws ?? 0;
                    playerGameLosses = player.PlayerSwissGameLosses ?? 0;

                    // Match-level statistics
                    playerMatchWins = player.PlayerSwissWins ?? 0;
                    playerMatchDraws = player.PlayerSwissDraws ?? 0;
                    playerMatchLosses = player.PlayerSwissLosses ?? 0;

                    // Derived match statistics
                    playerMatchPoints = player.PlayerSwissMatchPoints ?? ((playerMatchWins * 3) + playerMatchDraws);
                    playerMatchesPlayed = playerMatchWins + playerMatchDraws + playerMatchLosses;

                    // Game-level tracking
                    playerGamesPlayed = player.GamesPlayed ?? (playerGameWins + playerGameDraws + playerGameLosses);
                    playerGamePoints = (playerGameWins * 3) + playerGameDraws;
                }

                playerTotalGames = playerGameWins + playerGameDraws + playerGameLosses;


                // Stats
                double matchWinPercent = 0;
                double gameWinPercent = 0;
                double opMatchWinPercent = 0;
                double opGameWinPercent = 0;
                double opOpMatchWinPercent = 0;


                // Get opponents
                List<int> opponents = new List<int>();
                List<int> opponentsOpponents = new List<int>();

                List<PairingDto> relevantPairings = allPairings
                        .Where(p => (p.Player1Id == player.TournamentPlayerId)
                            || (p.Player2Id == player.TournamentPlayerId))
                        .ToList();

                Console.WriteLine(
                    $"Player {player.TournamentPlayerId}: relevantPairings={relevantPairings.Count}");

                opponents = relevantPairings
                    .Select(p => p.Player1Id == player.TournamentPlayerId ? p.Player2Id : p.Player1Id)
                    .Where(id => id.HasValue)
                    .Select(id => id!.Value)
                    .ToList();

                opponentsOpponents = allPairings
                    .Where(p => (opponents.Contains(p.Player1Id)
                            && p.Player2Id.HasValue)
                        || opponents.Contains((int)p.Player2Id!)
                    )
                    .Select(p =>
                        opponents.Contains(p.Player1Id) ? p.Player2Id : p.Player1Id)
                    .Where(id => id.HasValue)
                    .Select(id => id!.Value)
                    .Where(id => id != player.TournamentPlayerId) // remove self
                    .Where(id => !opponents.Contains(id))
                    .Distinct()
                    .ToList();

                Console.WriteLine($"Player {player.PlayerName}");
                Console.WriteLine($"Opponents found: {opponents.Count}");

                if (playerMatchesPlayed > 0)
                {
                    // Disqualified players dont count towards any calculation.
                    if (!player.TpDisqualified)
                    {
                        // Calculate match win percentage
                        // For Pokémon tournaments when finished, use special formula
                        if (tournamentGame == "pkmn" && tournamentIsFinished)
                        {
                            // If tournament is finished and player did not drop: wins / total rounds in tournament
                            // If tournament is finished and player dropped: wins / rounds in which they participated
                            int divisor = player.TpDropped ? (player.MatchesPlayed ?? 0) : maxRounds;

                            if (divisor > 0)
                            {
                                matchWinPercent = (double)playerGameWins / divisor * 100;

                                // Apply min/max constraints for PKMN
                                double minPercent = player.TpDropped ? 25.0 : 25.0;
                                double maxPercent = player.TpDropped ? 75.0 : 100.0;
                                matchWinPercent = Math.Max(minPercent, Math.Min(maxPercent, matchWinPercent));
                            }
                            else
                            {
                                matchWinPercent = 0;
                            }
                        }
                        else
                        {
                            // For MTG or when tournament is not finished, use standard calculation
                            matchWinPercent = playerMatchesPlayed > 0 ? (double)playerGameWins / playerMatchesPlayed * 100 : 0;
                        }

                        // Calculate game win percentage
                        // bye wins do not count, so exclude bye count from both numerator and denominator
                        int gamesWithoutByes = playerGamesPlayed - byeCount;
                        gameWinPercent =
                            gamesWithoutByes > 0
                                ? ((double)playerGameWins + (0.5 * playerGameDraws))
                                    / gamesWithoutByes * 100.0
                                : 0;

                        // Apply clamping for MTG tournaments
                        // MTG percentages are clamped to minimum 33.3% and maximum 100%
                        if (tournamentGame == "mtg")
                        {
                            matchWinPercent = Math.Max(33.3, Math.Min(100.0, matchWinPercent));
                            gameWinPercent = Math.Max(33.3, Math.Min(100.0, gameWinPercent));
                        }

                        // Calculate opponent match win percentage
                        // bye wins do not count towards opponent calculations
                        if (opponents.Count > 0)
                        {
                            double totalOpponentMatchWinPercent = 0;
                            int validOpponents = 0;

                            foreach (var opponentId in opponents)
                            {
                                var opponentPairings = allPairings
                                    .Where(p =>
                                        (p.Player1Id == opponentId || p.Player2Id == opponentId)
                                        && p.HasResult)
                                    .ToList();

                                // If opponent has no completed games, skip them
                                if (opponentPairings.Count == 0)
                                    continue;

                                int opponentWins = opponentPairings.Count(p =>
                                    (p.Player1Id == opponentId && p.Player1Score > p.Player2Score) ||
                                    (p.Player2Id == opponentId && p.Player2Score > p.Player1Score));

                                int opponentDraws = opponentPairings.Count(p =>
                                    p.Player1Score == p.Player2Score);

                                int opponentMatches = opponentPairings.Count;

                                // If still no valid matches, skip (safety)
                                if (opponentMatches == 0)
                                    continue;

                                totalOpponentMatchWinPercent += (double)opponentWins / opponentMatches * 100;
                                validOpponents++;
                            }

                            // Average out the opponent match percentages
                            opMatchWinPercent = validOpponents > 0 ? totalOpponentMatchWinPercent / validOpponents : 0;

                            // For MTG, apply clamping to opponent match win percentage
                            if (tournamentGame == "mtg")
                            {
                                opMatchWinPercent = Math.Max(33.3, Math.Min(100.0, opMatchWinPercent));
                            }
                        }

                        // Calculate Opponent Game Win Percentage (OGW%)
                        if (opponents.Count > 0)
                        {
                            double totalOpponentGameWinPercent = 0;
                            int validOpponents = 0;

                            foreach (var opponentId in opponents)
                            {
                                var opponentPlayer = tournamentPlayers
                                    .FirstOrDefault(tp => tp.TournamentPlayerId == opponentId);

                                if (opponentPlayer == null || opponentPlayer.TpDisqualified)
                                    continue;

                                int opponentGameWins;
                                int opponentGameDraws;
                                int opponentGameLosses;

                                if (tournamentFormat == "Swiss")
                                {
                                    opponentGameWins = opponentPlayer.PlayerSwissGameWins ?? 0;
                                    opponentGameDraws = opponentPlayer.PlayerSwissGameDraws ?? 0;
                                    opponentGameLosses = opponentPlayer.PlayerSwissGameLosses ?? 0;
                                }
                                else
                                {
                                    opponentGameWins = opponentPlayer.PlayerRoundRobinGameWins ?? 0;
                                    opponentGameDraws = opponentPlayer.PlayerRoundRobinGameDraws ?? 0;
                                    opponentGameLosses = opponentPlayer.PlayerRoundRobinGameLosses ?? 0;
                                }

                                int totalGames = opponentGameWins + opponentGameDraws + opponentGameLosses;
                                int opponentByeCount = opponentPlayer.PlayerBye ?? 0;
                                gamesWithoutByes = totalGames - opponentByeCount;

                                if (gamesWithoutByes <= 0)
                                    continue;

                                double opponentGameWinPercent =
                                    ((double)opponentGameWins + (0.5 * opponentGameDraws))
                                    / gamesWithoutByes
                                    * 100.0;

                                // MTG requires each opponent GWP to be floored at 33.3%
                                if (tournamentGame == "mtg")
                                {
                                    opponentGameWinPercent =
                                        Math.Max(33.3, opponentGameWinPercent);
                                }

                                totalOpponentGameWinPercent += opponentGameWinPercent;
                                validOpponents++;
                            }

                            opGameWinPercent =
                                validOpponents > 0
                                    ? totalOpponentGameWinPercent / validOpponents
                                    : 0;
                        }

                        // Calculate opponent's opponent match win percentage
                        // bye wins do not count
                        // Calculated in a second pass after all MatchWinPercent values exist.
                        opOpMatchWinPercent = 0;
                    }
                }


                // Calculate Opponent's Opponent Match Win %
                foreach (var tPlayer in tournamentPlayers)
                {
                    var tRelevantPairings = allPairings
                        .Where(p =>
                            p.Player1Id == tPlayer.TournamentPlayerId ||
                            p.Player2Id == tPlayer.TournamentPlayerId)
                        .ToList();

                    var tOpponents = tRelevantPairings
                        .Select(p =>
                            p.Player1Id == tPlayer.TournamentPlayerId
                                ? p.Player2Id
                                : p.Player1Id)
                        .Where(id => id.HasValue)
                        .Select(id => id!.Value)
                        .ToList();

                    var tOpponentsOpponents = allPairings
                        .Where(p =>
                            (tOpponents.Contains(p.Player1Id) && p.Player2Id.HasValue) ||
                            (p.Player2Id.HasValue && tOpponents.Contains(p.Player2Id.Value)))
                        .Select(p =>
                            tOpponents.Contains(p.Player1Id)
                                ? p.Player2Id
                                : p.Player1Id)
                        .Where(id => id.HasValue)
                        .Select(id => id!.Value)
                        .Where(id => id != player.TournamentPlayerId)
                        .Where(id => !tOpponents.Contains(id))
                        .Distinct()
                        .ToList();

                    double total = 0;
                    int validCount = 0;

                    foreach (var opOpId in tOpponentsOpponents)
                    {
                        if (standings.TryGetValue(opOpId, out var opOpStats))
                        {
                            if (!opOpStats.IsDisqualified)
                            {
                                total += opOpStats.MatchWinPercent;
                                validCount++;
                            }
                        }
                    }

                    opOpMatchWinPercent = validCount > 0 ? total / validCount : 0;
                }



                // Create initial standings dictionary for each player with values from database
                // And calculated values for points (wins * 3 + draws) and other stats
                standings[player.TournamentPlayerId] = new ITournamentScoringService.PlayerComputedStats
                {
                    TournamentPlayerId = player.TournamentPlayerId,
                    PlayerName = player.PlayerName ?? string.Empty,

                    Wins = playerGameWins,
                    Draws = playerGameDraws,
                    Losses = playerGameLosses,
                    MatchesPlayed = playerGameWins + playerGameDraws + playerGameLosses,
                    GamesPlayed = playerGamesPlayed,

                    MatchPoints = (playerGameWins * 3) + playerGameDraws,
                    MatchWinPercent = matchWinPercent,
                    GameWinPercent = gameWinPercent,
                    OpMatchWinPercent = opMatchWinPercent,
                    OpGameWinPercent = opGameWinPercent,
                    OpOpMatchWinPercent = opOpMatchWinPercent,

                    Position = 0,

                    IsDisqualified = player.TpDisqualified,
                    IsDropped = player.TpDropped,
                    Byes = byeCount
                };
            }

            // Extract all players from the standings dictionary into a working list for sorting
            var allPlayers = standings.Values.ToList();

            // If the game is MTG, sort by MTG tiebreaker rules
            if (tournamentGame == "mtg")
            {
                // Sort players in descending order by MTG tiebreaker rules
                // Sort by: MatchPoints > OpMatchWinPercent > GameWinPercent > OpGameWinPercent
                // Disqualified players do not get rankings
                // Tiebreakers are only applied within hierarchical groups at each level

                var activePlayers = new List<ITournamentScoringService.PlayerComputedStats>();

                var groupedByMatchPoints = allPlayers.Where(p => !p.IsDisqualified)
                    .GroupBy(p => p.MatchPoints)
                    .OrderByDescending(g => g.Key);

                foreach (var matchPointGroup in groupedByMatchPoints)
                {
                    var groupedByOpMatchWin = matchPointGroup
                        .GroupBy(p => Math.Round(p.OpMatchWinPercent, 3))
                        .OrderByDescending(g => g.Key);

                    foreach (var opMatchWinGroup in groupedByOpMatchWin)
                    {
                        var groupedByGameWin = opMatchWinGroup
                            .GroupBy(p => Math.Round(p.GameWinPercent, 3))
                            .OrderByDescending(g => g.Key);

                        foreach (var gameWinGroup in groupedByGameWin)
                        {
                            var sortedByOpGameWin = gameWinGroup
                                .OrderByDescending(p => p.OpGameWinPercent)
                                .ToList();

                            activePlayers.AddRange(sortedByOpGameWin);
                        }
                    }
                }

                // Assign positions, giving the same position to players with identical tiebreaker values
                int currentPosition = 1;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    // Check if this player has the same tiebreaker stats as the previous player
                    if (i > 0 && 
                        activePlayers[i].MatchPoints == activePlayers[i - 1].MatchPoints &&
                        Math.Abs(activePlayers[i].OpMatchWinPercent - activePlayers[i - 1].OpMatchWinPercent) < 0.001 &&
                        Math.Abs(activePlayers[i].GameWinPercent - activePlayers[i - 1].GameWinPercent) < 0.001 &&
                        Math.Abs(activePlayers[i].OpGameWinPercent - activePlayers[i - 1].OpGameWinPercent) < 0.001)
                    {
                        // Same position as previous player since all tiebreakers are equal
                        activePlayers[i].Position = activePlayers[i - 1].Position;
                    }
                    else
                    {
                        // New position starting from where we are in the list
                        currentPosition = i + 1;
                        activePlayers[i].Position = currentPosition;
                    }
                }

                // Disqualified players get no position (stays 0)
                foreach (var disqualifiedPlayer in allPlayers.Where(p => p.IsDisqualified))
                {
                    disqualifiedPlayer.Position = 0;
                }
            }
            // If the game is PKMN, sort by PKMN tiebreaker rules
            else if (tournamentGame == "pkmn")
            {
                // Sort by: OpMatchWinPercent > OpOpMatchWinPercent > HeadToHeadPosition (only if tournament finished)

                List<ITournamentScoringService.PlayerComputedStats> activePlayers;

                // Only include HeadToHeadPosition in sorting if tournament is finished
                if (tournamentIsFinished)
                {
                    activePlayers = new List<ITournamentScoringService.PlayerComputedStats>();

                    var groupedByMatchPoints = allPlayers.Where(p => !p.IsDisqualified)
                        .GroupBy(p => p.MatchPoints)
                        .OrderByDescending(g => g.Key);

                    foreach (var matchPointGroup in groupedByMatchPoints)
                    {
                        var groupedByOpMatchWin = matchPointGroup
                            .GroupBy(p => Math.Round(p.OpMatchWinPercent, 3))
                            .OrderByDescending(g => g.Key);

                        foreach (var opMatchWinGroup in groupedByOpMatchWin)
                        {
                            var groupedByOpOpMatchWin = opMatchWinGroup
                                .GroupBy(p => Math.Round(p.OpOpMatchWinPercent, 3))
                                .OrderByDescending(g => g.Key);

                            foreach (var opOpMatchWinGroup in groupedByOpOpMatchWin)
                            {
                                var sortedByHeadToHead = opOpMatchWinGroup
                                    .OrderBy(p => p.HeadToHeadPosition)
                                    .ToList();

                                activePlayers.AddRange(sortedByHeadToHead);
                            }
                        }
                    }
                }
                else
                {
                    // If tournament is not finished, do not use head-to-head; use default 0 position for tiebreaker
                    activePlayers = new List<ITournamentScoringService.PlayerComputedStats>();

                    var groupedByMatchPoints = allPlayers.Where(p => !p.IsDisqualified)
                        .GroupBy(p => p.MatchPoints)
                        .OrderByDescending(g => g.Key);

                    foreach (var matchPointGroup in groupedByMatchPoints)
                    {
                        var groupedByOpMatchWin = matchPointGroup
                            .GroupBy(p => Math.Round(p.OpMatchWinPercent, 3))
                            .OrderByDescending(g => g.Key);

                        foreach (var opMatchWinGroup in groupedByOpMatchWin)
                        {

                            var sortedByOpOpMatchWin = opMatchWinGroup
                                .OrderByDescending(p => p.OpOpMatchWinPercent)
                                .ToList();

                            activePlayers.AddRange(sortedByOpOpMatchWin);
                        }
                    }
                }

                // Assign positions, giving the same position to players with identical tiebreaker values
                int currentPosition = 1;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    // Check if this player has the same tiebreaker stats as the previous player
                    if (i > 0)
                    {
                        bool sameMatchPoints = activePlayers[i].MatchPoints == activePlayers[i - 1].MatchPoints;
                        bool sameOpMatchWin = Math.Abs(activePlayers[i].OpMatchWinPercent - activePlayers[i - 1].OpMatchWinPercent) < 0.001;
                        bool sameOpOpMatchWin = Math.Abs(activePlayers[i].OpOpMatchWinPercent - activePlayers[i - 1].OpOpMatchWinPercent) < 0.001;

                        // Include head-to-head comparison only if tournament is finished
                        bool sameHeadToHead = !tournamentIsFinished || activePlayers[i].HeadToHeadPosition == activePlayers[i - 1].HeadToHeadPosition;

                        if (sameMatchPoints && sameOpMatchWin && sameOpOpMatchWin && sameHeadToHead)
                        {
                            // Same position as previous player since all tiebreakers are equal
                            activePlayers[i].Position = activePlayers[i - 1].Position;
                        }
                        else
                        {
                            // New position starting from current list position
                            currentPosition = i + 1;
                            activePlayers[i].Position = currentPosition;
                        }
                    }
                    else
                    {
                        // First player gets position 1
                        activePlayers[i].Position = 1;
                    }
                }

                // Disqualified players get no position
                foreach (var disqualifiedPlayer in allPlayers.Where(p => p.IsDisqualified))

                {
                    disqualifiedPlayer.Position = 0;
                }
            }

            await SavePositions(standings);
            return standings;
        }

        // Saves each tournamentplayer's position from their own PlayerComputedStats
        public async Task<bool> SavePositions(Dictionary<int, ITournamentScoringService.PlayerComputedStats> players)
        {
            try
            {
                // Update each player's position in the database using the tournament player service
                // and the PlayerComputedStats from the ComputeTournamentStandings method
                foreach (var player in players.Values)
                {
                    var tp = await _tpService.GetByIdAsync(player.TournamentPlayerId);
                    if (tp != null)
                    {
                        tp.TpPosition = player.Position;
                        await _tpService.UpdateAsync(tp);
                    }
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}


