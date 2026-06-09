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
            var players = (await _tpService.GetAllWhereAsync(tp => tp.TournamentId == tournamentId)).ToList();
            var pairings = (await _pairingService.GetAllWhereAsync(p => p.TournamentId == tournamentId)).ToList();

            var stats = players.Select(p => new PlayerComputedStats
            {
                TournamentPlayerId = p.TournamentPlayerId,
                Wins = 0,
                Draws = 0,
                Losses = 0,
                Points = 0,
                OmwPercent = 0,
                GameWinPercent = 0,
                GamesWon = p.GamesWon ?? 0,
                GamesPlayed = p.GamesPlayed ?? 0
            }).ToDictionary(x => x.TournamentPlayerId);

            // Process pairings with results
            foreach (var pairing in pairings.Where(x => x.PairingHasResult))
            {
                var p1 = pairing.PairingTp1;
                var p2 = pairing.PairingTp2;

                if (p2 == null)
                {
                    // bye -> award a win to p1
                    if (p1.HasValue && stats.ContainsKey(p1.Value))
                    {
                        stats[p1.Value].Wins += 1;
                        stats[p1.Value].Points += 3;
                    }

                    continue;
                }

                if (!pairing.PairingPlayer1Score.HasValue || !pairing.PairingPlayer2Score.HasValue)
                    continue;

                var s1 = pairing.PairingPlayer1Score.Value;
                var s2 = pairing.PairingPlayer2Score.Value;

                if (s1 > s2)
                {
                    if (p1.HasValue) { stats[p1.Value].Wins += 1; stats[p1.Value].Points += 3; }
                    if (p2.HasValue) { stats[p2.Value].Losses += 1; }
                }
                else if (s1 < s2)
                {
                    if (p2.HasValue) { stats[p2.Value].Wins += 1; stats[p2.Value].Points += 3; }
                    if (p1.HasValue) { stats[p1.Value].Losses += 1; }
                }
                else
                {
                    if (p1.HasValue) { stats[p1.Value].Draws += 1; stats[p1.Value].Points += 1; }
                    if (p2.HasValue) { stats[p2.Value].Draws += 1; stats[p2.Value].Points += 1; }
                }

                // game counts
                if (pairing.PairingPlayer1GameCount.HasValue && pairing.PairingPlayer2GameCount.HasValue)
                {
                    if (p1.HasValue) { stats[p1.Value].GamesPlayed += pairing.PairingPlayer1GameCount.Value + pairing.PairingPlayer2GameCount.Value; stats[p1.Value].GamesWon += pairing.PairingPlayer1GameCount.Value; }
                    if (p2.HasValue) { stats[p2.Value].GamesPlayed += pairing.PairingPlayer1GameCount.Value + pairing.PairingPlayer2GameCount.Value; stats[p2.Value].GamesWon += pairing.PairingPlayer2GameCount.Value; }
                }
            }

            // Compute OMW% for each player
            foreach (var s in stats.Values)
            {
                var playerPairings = pairings.Where(x => x.PairingHasResult)
                    .Where(x => x.PairingTp1 == s.TournamentPlayerId || x.PairingTp2 == s.TournamentPlayerId)
                    .ToList();

                var opponentIds = playerPairings.Select(x => x.PairingTp1 == s.TournamentPlayerId ? x.PairingTp2 : x.PairingTp1).Where(id => id.HasValue).Select(id => id!.Value).ToList();

                var opponentWinsSum = opponentIds.Sum(id => stats.ContainsKey(id) ? stats[id].Wins : 0);
                var opponentMatchesCompleted = opponentIds.Sum(id => stats.ContainsKey(id) ? stats[id].Wins + stats[id].Draws + stats[id].Losses : 0);

                s.OmwPercent = opponentMatchesCompleted == 0 ? 0 : (opponentWinsSum / (double)opponentMatchesCompleted) * 100.0;

                s.GameWinPercent = s.GamesPlayed == 0 ? 0 : (s.GamesWon / (double)s.GamesPlayed) * 100.0;
            }

            return stats.Values.OrderByDescending(x => x.Points).ThenByDescending(x => x.OmwPercent).ToList();
        }
    }
}
