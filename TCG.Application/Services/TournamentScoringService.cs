using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Interfaces.Services;

namespace TCG.Application.Services
{
    public class TournamentScoringService
    {
        private readonly ITournamentPlayerService _tpService;
        private readonly IPairingService _pairingService;

        public TournamentScoringService(ITournamentPlayerService tpService, IPairingService pairingService)
        {
            _tpService = tpService;
            _pairingService = pairingService;
        }

        public class PlayerComputedStats
        {
            public int TournamentPlayerId { get; set; }
            public int Wins { get; set; }
            public int Draws { get; set; }
            public int Losses { get; set; }
            public int Points { get; set; } // wins = 3 + draws = 1
            public double OmwPercent { get; set; }
            public double GameWinPercent { get; set; }
            public int GamesWon { get; set; }
            public int GamesPlayed { get; set; }
        }

        // Computes standings and on-demand OMW% and game win% for all players in the tournament
        public async Task<List<PlayerComputedStats>> ComputeAllAsync(int tournamentId)
        {
            var players = (await _tpService.GetAllWhereAsync(tp => tp.TournamentId == tournamentId))
                .ToList();

            var pairings = (await _pairingService.GetAllWhereAsync(p => p.TournamentId == tournamentId))
                .Where(p => p.PairingHasResult)
                .ToList();

            var stats = players.ToDictionary(
                p => p.TournamentPlayerId,
                p => new PlayerComputedStats
                {
                    TournamentPlayerId = p.TournamentPlayerId,
                    GamesWon = p.GamesWon ?? 0,
                    GamesPlayed = p.GamesPlayed ?? 0
                });

            // Track opponents per player (for OMW)
            var opponents = players.ToDictionary(
                p => p.TournamentPlayerId,
                _ => new List<int>());

            foreach (var pairing in pairings)
            {
                var p1 = pairing.PairingTp1;
                var p2 = pairing.PairingTp2;

                // BYE
                if (p2 == null)
                {
                    if (p1.HasValue)
                    {
                        stats[p1.Value].Wins++;
                        stats[p1.Value].Points += 3;
                    }
                    continue;
                }

                if (!pairing.PairingPlayer1Score.HasValue ||
                    !pairing.PairingPlayer2Score.HasValue)
                    continue;

                var s1 = pairing.PairingPlayer1Score.Value;
                var s2 = pairing.PairingPlayer2Score.Value;

                if (p1.HasValue && p2.HasValue)
                {
                    opponents[p1.Value].Add(p2.Value);
                    opponents[p2.Value].Add(p1.Value);

                    if (s1 > s2)
                    {
                        stats[p1.Value].Wins++;
                        stats[p1.Value].Points += 3;
                        stats[p2.Value].Losses++;
                    }
                    else if (s1 < s2)
                    {
                        stats[p2.Value].Wins++;
                        stats[p2.Value].Points += 3;
                        stats[p1.Value].Losses++;
                    }
                    else
                    {
                        stats[p1.Value].Draws++;
                        stats[p2.Value].Draws++;
                        stats[p1.Value].Points += 1;
                        stats[p2.Value].Points += 1;
                    }

                    // Game stats
                    if (pairing.PairingPlayer1GameCount.HasValue &&
                        pairing.PairingPlayer2GameCount.HasValue)
                    {
                        stats[p1.Value].GamesPlayed +=
                            pairing.PairingPlayer1GameCount.Value +
                            pairing.PairingPlayer2GameCount.Value;

                        stats[p2.Value].GamesPlayed +=
                            pairing.PairingPlayer1GameCount.Value +
                            pairing.PairingPlayer2GameCount.Value;

                        stats[p1.Value].GamesWon += pairing.PairingPlayer1GameCount.Value;
                        stats[p2.Value].GamesWon += pairing.PairingPlayer2GameCount.Value;
                    }
                }
            }

            // Compute OMW + Game Win %
            foreach (var s in stats.Values)
            {
                var opps = opponents[s.TournamentPlayerId];

                var oppStats = opps
                    .Where(id => stats.ContainsKey(id))
                    .Select(id => stats[id])
                    .ToList();

                var oppWins = oppStats.Sum(x => x.Wins);
                var oppGames = oppStats.Sum(x => x.Wins + x.Draws + x.Losses);

                s.OmwPercent = oppGames == 0
                    ? 0
                    : (oppWins / (double)oppGames) * 100.0;

                s.GameWinPercent = s.GamesPlayed == 0
                    ? 0
                    : (s.GamesWon / (double)s.GamesPlayed) * 100.0;
            }

            return stats.Values
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.OmwPercent)
                .ToList();
        }
    }
}
