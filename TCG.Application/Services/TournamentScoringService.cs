using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                int gamesPlayed,
                    matchesPlayed,
                    gameWins,
                    gameDraws,
                    gameLosses,
                    matchPoints;

                int byeCount = player.PlayerBye ?? 0;

                //byeCount = relevantPairings.Count(p => p.Player2Id == null
                //    && p.Player1Id == player.TournamentPlayerId);

                if (tournamentFormat == "RoundRobin")
                {
                    gamesPlayed = player.PlayerRoundRobinWins ?? 0;
                    matchesPlayed = player.PlayerRoundRobinDraws ?? 0;
                    gameWins = player.PlayerRoundRobinWins ?? 0;
                    gameDraws = player.PlayerRoundRobinDraws ?? 0;
                    gameLosses = player.PlayerRoundRobinLosses ?? 0;
                    matchPoints = player.PlayerRoundRobinDraws ?? 0;
                }
                else
                {
                    gamesPlayed = player.PlayerSwissWins ?? 0;
                    matchesPlayed = player.PlayerSwissDraws ?? 0;
                    gameWins = player.PlayerSwissWins ?? 0;
                    gameDraws = player.PlayerSwissDraws ?? 0;
                    gameLosses = player.PlayerSwissLosses ?? 0;
                    matchPoints = player.PlayerSwissDraws ?? 0;
                }

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

                
                
                if (player.GamesPlayed > 0 && matchesPlayed > 0)
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
                                matchWinPercent = (double)gameWins / divisor * 100;

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
                            matchWinPercent = matchesPlayed > 0 ? (double)gameWins / matchesPlayed * 100 : 0;
                        }

                        // Calculate game win percentage
                        // bye wins do not count, so we exclude bye count from both numerator and denominator
                        int gamesWithoutByes = gamesPlayed - byeCount;
                        gameWinPercent = gamesWithoutByes > 0 ? (double)gameWins / gamesWithoutByes * 100 : 0;

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
                                // Find the opponent player from tournament players to get their stats
                                var opponentPlayer = tournamentPlayers.FirstOrDefault(tp => tp.TournamentPlayerId == opponentId);
                                if (opponentPlayer != null && !opponentPlayer.TpDisqualified)
                                {
                                    // Get opponent's match record without byes
                                    int opponentMatches = (opponentPlayer.PlayerRoundRobinWins ?? 0) + 
                                                        (opponentPlayer.PlayerRoundRobinDraws ?? 0) + 
                                                        (opponentPlayer.PlayerRoundRobinLosses ?? 0);

                                    if (tournamentFormat == "Swiss")
                                    {
                                        opponentMatches = (opponentPlayer.PlayerSwissWins ?? 0) + 
                                                        (opponentPlayer.PlayerSwissDraws ?? 0) + 
                                                        (opponentPlayer.PlayerSwissLosses ?? 0);
                                    }

                                    int opponentWins = (opponentPlayer.PlayerRoundRobinWins ?? 0);
                                    if (tournamentFormat == "Swiss")
                                    {
                                        opponentWins = (opponentPlayer.PlayerSwissWins ?? 0);
                                    }

                                    int opponentByes = opponentPlayer.PlayerBye ?? 0;
                                    int opponentGamesWithoutByes = (opponentPlayer.GamesPlayed ?? 0) - opponentByes;

                                    // Only count opponent if they have played matches
                                    if (opponentMatches > 0)
                                    {
                                        totalOpponentMatchWinPercent += (double)opponentWins / opponentMatches * 100;
                                        validOpponents++;
                                    }
                                }
                            }

                            // Average out the opponent match percentages
                            opMatchWinPercent = validOpponents > 0 ? totalOpponentMatchWinPercent / validOpponents : 0;

                            // For MTG, apply clamping to opponent match win percentage
                            if (tournamentGame == "mtg")
                            {
                                opMatchWinPercent = Math.Max(33.3, Math.Min(100.0, opMatchWinPercent));
                            }
                        }

                        // Calculate opponent game win percentage
                        // bye wins do not count
                        if (opponents.Count > 0)
                        {
                            double totalOpponentGameWinPercent = 0;
                            int validOpponents = 0;

                            foreach (var opponentId in opponents)
                            {
                                var opponentPlayer = tournamentPlayers.FirstOrDefault(tp => tp.TournamentPlayerId == opponentId);
                                if (opponentPlayer != null && !opponentPlayer.TpDisqualified)
                                {
                                    int opponentByes = opponentPlayer.PlayerBye ?? 0;
                                    int opponentGamesWithoutByes = (opponentPlayer.GamesPlayed ?? 0) - opponentByes;

                                    int opponentGameWins = (opponentPlayer.PlayerRoundRobinWins ?? 0);
                                    if (tournamentFormat == "Swiss")
                                    {
                                        opponentGameWins = (opponentPlayer.PlayerSwissWins ?? 0);
                                    }

                                    if (opponentGamesWithoutByes > 0)
                                    {
                                        totalOpponentGameWinPercent += (double)opponentGameWins / opponentGamesWithoutByes * 100;
                                        validOpponents++;
                                    }
                                }
                            }

                            opGameWinPercent = validOpponents > 0 ? totalOpponentGameWinPercent / validOpponents : 0;

                            // For MTG, apply clamping to opponent game win percentage
                            if (tournamentGame == "mtg")
                            {
                                opGameWinPercent = Math.Max(33.3, Math.Min(100.0, opGameWinPercent));
                            }
                        }

                        // Calculate opponent's opponent match win percentage
                        // bye wins do not count
                        if (opponentsOpponents.Count > 0)
                        {
                            double totalOpOpMatchWinPercent = 0;
                            int validOpOpponents = 0;

                            foreach (var opOpId in opponentsOpponents)
                            {
                                var opOpPlayer = tournamentPlayers.FirstOrDefault(tp => tp.TournamentPlayerId == opOpId);
                                if (opOpPlayer != null && !opOpPlayer.TpDisqualified)
                                {
                                    int opOpMatches = (opOpPlayer.PlayerRoundRobinWins ?? 0) + 
                                                    (opOpPlayer.PlayerRoundRobinDraws ?? 0) + 
                                                    (opOpPlayer.PlayerRoundRobinLosses ?? 0);

                                    if (tournamentFormat == "Swiss")
                                    {
                                        opOpMatches = (opOpPlayer.PlayerSwissWins ?? 0) + 
                                                    (opOpPlayer.PlayerSwissDraws ?? 0) + 
                                                    (opOpPlayer.PlayerSwissLosses ?? 0);
                                    }

                                    int opOpWins = (opOpPlayer.PlayerRoundRobinWins ?? 0);
                                    if (tournamentFormat == "Swiss")
                                    {
                                        opOpWins = (opOpPlayer.PlayerSwissWins ?? 0);
                                    }

                                    if (opOpMatches > 0)
                                    {
                                        totalOpOpMatchWinPercent += (double)opOpWins / opOpMatches * 100;
                                        validOpOpponents++;
                                    }
                                }
                            }

                            opOpMatchWinPercent = validOpOpponents > 0 ? totalOpOpMatchWinPercent / validOpOpponents : 0;
                        }
                    }
                }

                // Create initial standings dictionary for each player with values from database
                // And calculated values for points (wins * 3 + draws) and other stats
                standings[player.TournamentPlayerId] = new ITournamentScoringService.PlayerComputedStats
                {
                    TournamentPlayerId = player.TournamentPlayerId,
                    PlayerName = player.PlayerName ?? string.Empty,

                    Wins = gameWins,
                    Draws = gameDraws,
                    Losses = gameLosses,
                    MatchesPlayed = gameWins + gameDraws + gameLosses,
                    GamesPlayed = gamesPlayed,

                    MatchPoints = (gameWins * 3) + gameDraws,
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
                var activePlayers = allPlayers.Where(p => !p.IsDisqualified).OrderByDescending(p => p.MatchPoints)
                    .ThenByDescending(p => p.OpMatchWinPercent)
                    .ThenByDescending(p => p.GameWinPercent)
                    .ThenByDescending(p => p.OpGameWinPercent)
                    .ToList();

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
                // Sort players in descending order by PKMN tiebreaker rules
                // Sort by: OpMatchWinPercent > OpOpMatchWinPercent > HeadToHeadPosition (only if tournament finished)

                List<ITournamentScoringService.PlayerComputedStats> activePlayers;

                // Only include HeadToHeadPosition in sorting if tournament is finished
                if (tournamentIsFinished)
                {
                    activePlayers = allPlayers.Where(p => !p.IsDisqualified)
                        .OrderByDescending(p => p.OpMatchWinPercent)
                        .ThenByDescending(p => p.OpOpMatchWinPercent)
                        .ThenBy(p => p.HeadToHeadPosition)
                        .ToList();
                }
                else
                {
                    // If tournament is not finished, do not use head-to-head; use default 0 position for tiebreaker
                    activePlayers = allPlayers.Where(p => !p.IsDisqualified)
                        .OrderByDescending(p => p.OpMatchWinPercent)
                        .ThenByDescending(p => p.OpOpMatchWinPercent)
                        .ToList();
                }

                // Assign positions, giving the same position to players with identical tiebreaker values
                int currentPosition = 1;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    // Check if this player has the same tiebreaker stats as the previous player
                    if (i > 0)
                    {
                        bool sameOpMatchWin = Math.Abs(activePlayers[i].OpMatchWinPercent - activePlayers[i - 1].OpMatchWinPercent) < 0.001;
                        bool sameOpOpMatchWin = Math.Abs(activePlayers[i].OpOpMatchWinPercent - activePlayers[i - 1].OpOpMatchWinPercent) < 0.001;

                        // Include head-to-head comparison only if tournament is finished
                        bool sameHeadToHead = !tournamentIsFinished || activePlayers[i].HeadToHeadPosition == activePlayers[i - 1].HeadToHeadPosition;

                        if (sameOpMatchWin && sameOpOpMatchWin && sameHeadToHead)
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
                    else
                    {
                        // First player gets position 1
                        activePlayers[i].Position = 1;
                    }
                }

                // Disqualified players get no position (stays 0)
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
