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

        public async Task<List<Dictionary<int, ITournamentScoringService.PlayerComputedStats>>> ComputeTournamentStandings(int tournamentId)
        {
            // Load tournament players from async service
            var tournamentPlayers = await _tpService.GetAllWhereAsync(tp => tp.TournamentId == tournamentId);

            var allPairings = await _pService.GetAllWhereAsync(p => p.TournamentId == tournamentId);

            var tournamentGame = (await _tService.GetByIdAsync(tournamentId)).TournamentGame;

            var tournamentFormat = (await _tService.GetByIdAsync(tournamentId)).TournamentFormat;

            var standings = new List<Dictionary<int, ITournamentScoringService.PlayerComputedStats>>();


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

                    // Calculate match win percentage


                    // Calculate game win percentage
                    // bye wins do not count


                    // Calculate opponent match win percentage
                    // bye wins do not count


                    // Calculate opponent game win percentage
                    // bye wins do not count


                    // Calculate opponent's opponent match win percentage
                    // bye wins do not count


                }

                // Create initial standings dictionary for each player with values from database
                // And calculated values for points (wins * 3 + draws) and other stats
                standings.Add(new Dictionary<int, ITournamentScoringService.PlayerComputedStats>
                {
                    [player.TournamentPlayerId] = new ITournamentScoringService.PlayerComputedStats
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
                    }
                });
            }

            // {

            // pkmn and mtg tourneys, disqualified players do not count towards any tie breaks and they
            // are not set any rankings. dropped players are treated the same as non-dropped non-disqualified players
            // For pkmn tourneys only {
            // Select the players where IsDisqualified or IsDropped is false
            //  and MatchWinPercent OpMatchWinPercent and OpOpMatchWinPercent
            // are all equal
            // Follow the below head-to-head login
            //            If exactly two competitors are tied in the final standings and those competitors played each other
            // during the tournament, then the winner of that match is ranked higher than the loser.
            // If exactly two competitors are tied in the final standings and those competitors did not play each
            // other during the tournament, then the order in which they appear will be randomly determined.
            //
            // If more than two competitors are tied in the final standings, then the order in which they appear
            // will be randomly determined.
            // To store the head-to-head tie results, use HeadToHeadPosition for each standing dictionary.
            // the person that comes 1st will be 1, 2nd will be 2, 3rd will be 3, etc. Head-to-head random decisions will be done ONLY when the tournament is marked as finished in the database, and random calculations are done using the tournament's saved seed. In the meantime, players who are tied to the point of needing head-to-head to be resolved are not resolved and are just given the same position.
            // }
            // 

            // And then, positions will be set for both mtg and pkmn tourney.

            // For mtg, it will be in descending order depending on the stats from Match points, then
            // Opponents’ match - win percentage, then Game - win percentage,
            // then Opponents’ game - win percentage. if there is a tie, then both players will have the
            // same position.

            // For pkmn, it will be in descending order depending on the stats from OpMatchWinPercent,
            // then OpOpMatchWinPercent, then HeadToHeadPosition.

            // for both pkmn and mtg tourneys, if when comparing one stat from one player turns out to be higher
            // than the other player's stat, subsequent comparisons to determine positions will
            // not be done, and positions will be determined from the stats were actually compared.
            // }


            // Identify players who are disqualified or dropped from the first standings dictionary
            if (standings.Count > 0)
            {
                var firstDict = standings[0];
                List<int> tiedPlayers = firstDict
                    .Where(s => s.Value.IsDisqualified || s.Value.IsDropped)
                    .Select(s => s.Key)
                    .ToList();
            }

            return standings;
        }

        // Saves each tournamentplayer's position from their own PlayerComputedStats
        public async Task<bool> SavePositions(Dictionary<int, ITournamentScoringService.PlayerComputedStats> players)
        {
            throw new NotImplementedException();
        }
    }
}