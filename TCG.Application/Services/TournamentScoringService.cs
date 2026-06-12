using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Application.Interfaces;
using TCG.Application.Interfaces.Services;

namespace TCG.Application.Services
{
    public class TournamentScoringService
         //: ITournamentScoringService
    {
        private readonly ITournamentPlayerService _tpService;
        private readonly IPairingService _pairingService;

        private readonly Random _rnd;

        public TournamentScoringService(ITournamentPlayerService tpService, IPairingService pairingService)
        {
            _tpService = tpService;
            _pairingService = pairingService;
            _rnd = new Random();
        }

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


        // Returns a dictionary consisting of player info and stats
        // public Dictionary<int, PlayerComputedStats> ComputeTournamentStandings(
        public void ComputeTournamentStandings(
            int tournamentId
        )
        {
            return;
        }

        // Organizes the computed stats for display for MTG tournaments
        // public Dictionary<int, PlayerComputedStats> OrganizeMtgStatsForDisplay(
        public void OrganizeMtgStatsForDisplay(
            int tournamentId,
            Dictionary<int, PlayerComputedStats> standings
        )
        {
            return;
        }

        // Organizes the computed stats for display for PKMN tournaments
        // public Dictionary<int, PlayerComputedStats> OrganizePkmnStatsForDisplay(
        public void OrganizePkmnStatsForDisplay(
            int tournamentId,
            Dictionary<int, PlayerComputedStats> standings
        )
        {
            return;
        }

        // Saves rankings
        public bool SavePositions(
            Dictionary<int, PlayerComputedStats> players
        )
        {
            return false;
        }








        // Helper: initialize stats dictionary, opponents map and byeRounds map
        private (Dictionary<int, PlayerComputedStats> stats, Dictionary<int, List<int>> opponents, Dictionary<int, List<int>> byeRounds)
            InitializeStatsAndTrackers(IEnumerable<TournamentPlayerDto> players)
        {
            var stats = new Dictionary<int, PlayerComputedStats>();
            foreach (var player in players)
            {
                stats[player.TournamentPlayerId] = new PlayerComputedStats
                {
                    TournamentPlayerId = player.TournamentPlayerId,
                    GamesWon = player.GamesWon ?? 0,
                    GamesPlayed = player.GamesPlayed ?? 0,
                    IsDisqualified = player.TpDisqualified,
                    IsDropped = player.TpDropped
                };
            }

            var opponents = players.ToDictionary(p => p.TournamentPlayerId, _ => new List<int>());
            var byeRounds = players.ToDictionary(p => p.TournamentPlayerId, _ => new List<int>());

            return (stats, opponents, byeRounds);
        }

        // Helper: whether a tournament player id is active (not disqualified/dropped)
        private bool IsActive(Dictionary<int, PlayerComputedStats> stats, int id)
            => stats.ContainsKey(id) && !stats[id].IsDisqualified && !stats[id].IsDropped;

        // Helper: process a pairing and update stats, opponents and byeRounds
        private void ProcessPairing(TCG.Application.Dtos.PairingDto pairing, Dictionary<int, PlayerComputedStats> stats,
            Dictionary<int, List<int>> opponents, Dictionary<int, List<int>> byeRounds, int roundNumber)
        {
            var p1 = pairing.PairingTp1;
            var p2 = pairing.PairingTp2;

            // BYE case - player number 2 is null
            if (p2 == null)
            {
                if (p1.HasValue && stats.ContainsKey(p1.Value))
                {
                    stats[p1.Value].Wins++;
                    stats[p1.Value].Points += 3;
                    byeRounds[p1.Value].Add(roundNumber);
                }
                return;
            }

            if (!pairing.PairingPlayer1Score.HasValue || !pairing.PairingPlayer2Score.HasValue)
                return;

            var s1 = pairing.PairingPlayer1Score.Value;
            var s2 = pairing.PairingPlayer2Score.Value;

            if (!p1.HasValue || !p2.HasValue || !stats.ContainsKey(p1.Value) || !stats.ContainsKey(p2.Value))
                return;

            // Only record opponent if neither player is disqualified or dropped
            if (IsActive(stats, p1.Value) && IsActive(stats, p2.Value))
            {
                opponents[p1.Value].Add(p2.Value);
                opponents[p2.Value].Add(p1.Value);
            }

            // Determine match winner and update stats
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

            // Record game statistics for the match
            if (pairing.PairingPlayer1GameCount.HasValue && pairing.PairingPlayer2GameCount.HasValue)
            {
                int totalGamesInMatch = pairing.PairingPlayer1GameCount.Value + pairing.PairingPlayer2GameCount.Value;

                stats[p1.Value].GamesPlayed += totalGamesInMatch;
                stats[p2.Value].GamesPlayed += totalGamesInMatch;

                stats[p1.Value].GamesWon += pairing.PairingPlayer1GameCount.Value;
                stats[p2.Value].GamesWon += pairing.PairingPlayer2GameCount.Value;
            }
        }

        // Helper: resolve two-way head-to-head ordering; returns ordered pair
        private List<PlayerComputedStats> ResolveTwoWayHeadToHead(PlayerComputedStats a, PlayerComputedStats b, List<TCG.Application.Dtos.PairingDto> pairings)
        {
            var ph = pairings.FirstOrDefault(p => p.PairingHasResult
                && ((p.PairingTp1 == a.TournamentPlayerId && p.PairingTp2 == b.TournamentPlayerId)
                    || (p.PairingTp1 == b.TournamentPlayerId && p.PairingTp2 == a.TournamentPlayerId)));
            if (ph != null && ph.PairingPlayer1Score.HasValue && ph.PairingPlayer2Score.HasValue)
            {
                if (ph.PairingPlayer1Score.Value > ph.PairingPlayer2Score.Value)
                    return ph.PairingTp1 == a.TournamentPlayerId ? new List<PlayerComputedStats> { a, b } : new List<PlayerComputedStats> { b, a };
                if (ph.PairingPlayer1Score.Value < ph.PairingPlayer2Score.Value)
                    return ph.PairingTp1 == a.TournamentPlayerId ? new List<PlayerComputedStats> { b, a } : new List<PlayerComputedStats> { a, b };
            }
            // no decisive head-to-head or no match -> random coin flip
            return new List<PlayerComputedStats> { a, b }.OrderBy(_ => _rnd.Next()).ToList();
        }

        // Computes standings and stats and tiebreakers for all players in the tournament
        public async Task<List<PlayerComputedStats>> ComputeAllAsync(int tournamentId)
        {
            var players = (await _tpService.GetAllWhereAsync(tp => tp.TournamentId == tournamentId))
                .ToList();

            var pairings = (await _pairingService.GetAllWhereAsync(p => p.TournamentId == tournamentId))
                .Where(p => p.PairingHasResult)
                .ToList();

            // Initialize stats, opponents and bye trackers
            var (stats, opponents, byeRounds) = InitializeStatsAndTrackers(players);

            // Process all pairings to compute wins, losses, draws, and points
            int roundNumber = 0;
            foreach (var pairing in pairings)
            {
                ProcessPairing(pairing, stats, opponents, byeRounds, roundNumber);
                roundNumber++;
            }

            // Compute opponent win percentage
            ComputeTiebreakers(stats, opponents, byeRounds);

            return stats.Values.ToList();
        }

        // Calculates opponent win percentage and game stats
        private void ComputeTiebreakers(
            Dictionary<int, PlayerComputedStats> stats,
            Dictionary<int, List<int>> opponents,
            Dictionary<int, List<int>> byeRounds)
        {
            // First pass: compute Op Win% for all players (not including disqualified/dropped)
            foreach (var s in stats.Values)
            {
                if (s.IsDisqualified || s.IsDropped)
                {
                    s.OpWinPercent = 0;
                    s.GameWinPercent = 0;
                    s.OpGameWinPercent = 0;
                    continue;
                }

                // Compute opponent win percentage, ignoring opponents' byes
                var opps = opponents[s.TournamentPlayerId];
                var activeOpponents = opps
                    .Where(id => stats.ContainsKey(id) && !stats[id].IsDisqualified && !stats[id].IsDropped)
                    .ToList();

                if (activeOpponents.Count > 0)
                {
                    double totalOppWins = 0;
                    double totalOppMatches = 0;
                    double totalOppGameWinPercent = 0;

                    foreach (var oppId in activeOpponents)
                    {
                        var opp = stats[oppId];
                        // Count how many byes this opponent had so we can exclude them
                        int oppByes = byeRounds.ContainsKey(oppId) ? byeRounds[oppId].Count : 0;

                        var oppWins = opp.Wins - oppByes; // remove bye wins
                        var oppMatches = (opp.Wins + opp.Draws + opp.Losses) - oppByes; // remove bye matches

                        if (oppMatches < 0) oppMatches = 0;
                        if (oppWins < 0) oppWins = 0;

                        totalOppWins += oppWins;
                        totalOppMatches += oppMatches;

                        // accumulate opponent's game win percent (game stats already ignore byes because byes have no games)
                        totalOppGameWinPercent += (opp.GamesPlayed == 0) ? 0 : (opp.GamesWon / (double)opp.GamesPlayed) * 100.0;
                    }

                    if (totalOppMatches > 0)
                    {
                        s.OpWinPercent = Math.Max(25.0, Math.Min(100.0, (totalOppWins / totalOppMatches) * 100.0));
                    }
                    else
                    {
                        s.OpWinPercent = 25.0;
                    }

                    // Average opponents' game win percentages
                    s.OpGameWinPercent = activeOpponents.Count > 0
                        ? totalOppGameWinPercent / activeOpponents.Count
                        : 0;
                }
                else
                {
                    s.OpWinPercent = 25.0;
                    s.OpGameWinPercent = 0;
                }

                // Compute game win percentage for this player
                s.GameWinPercent = s.GamesPlayed == 0
                    ? 0
                    : (s.GamesWon / (double)s.GamesPlayed) * 100.0;
            }

            // Second pass: (no additional tiebreakers required)
            // foreach (var s in stats.Values) { /* no-op */ }
        }

        // Computes the final rankings for PKMN tournaments using PKMN-specific tiebreakers
        public List<PlayerComputedStats> ComputePkmnRankings(List<PlayerComputedStats> stats, int maxRoundsCompleted)
        {
            // Separate disqualified/dropped players from active players
            var activeStats = stats
                .Where(s => !s.IsDisqualified && !s.IsDropped)
                .ToList();

            var disqualifiedStats = stats
                .Where(s => s.IsDisqualified)
                .OrderBy(s => s.TournamentPlayerId)
                .ToList();

            var droppedStats = stats
                .Where(s => s.IsDropped && !s.IsDisqualified)
                .OrderBy(s => s.TournamentPlayerId)
                .ToList();

            // Sort active players by PKMN tiebreaker rules: Points then Op Win%
            var sorted = activeStats
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.OpWinPercent)
                .ToList();

            // Assign positions to active players
            int position = 1;
            for (int i = 0; i < sorted.Count; i++)
            {
                sorted[i].Position = position++;
            }

            // Dropped players follow active players
            for (int i = 0; i < droppedStats.Count; i++)
            {
                droppedStats[i].Position = position++;
            }

            // Disqualified players follow dropped players
            for (int i = 0; i < disqualifiedStats.Count; i++)
            {
                disqualifiedStats[i].Position = position++;
            }

            // Combine all results in order
            var finalResult = new List<PlayerComputedStats>();
            finalResult.AddRange(sorted);
            finalResult.AddRange(droppedStats);
            finalResult.AddRange(disqualifiedStats);

            return finalResult;
        }

        // Returns true if there are PKMN tiebreak groups that need to be resolved once the tournament has reached max rounds
        public async Task<bool> HasPendingPkmnTiebreaksAsync(int tournamentId, int currentRounds, int? maxRounds)
        {
            if (!maxRounds.HasValue) return false;
            if (currentRounds < maxRounds.Value) return false;

            var stats = await ComputeAllAsync(tournamentId);

            // group active players by points and opwin
            var active = stats.Where(s => !s.IsDisqualified && !s.IsDropped)
                .ToList();

            var groups = active.GroupBy(s => new { s.Points, Op = Math.Round(s.OpWinPercent, 4) })
                .Where(g => g.Count() > 1)
                .ToList();

            return groups.Count > 0;
        }

        // Resolve final PKMN ties once tournament ended. Head-to-head used if available, otherwise randomise.
        public async Task<List<PlayerComputedStats>> ResolvePkmnFinalTiesAsync(List<PlayerComputedStats> stats, int tournamentId, int currentRounds, int? maxRounds)
        {
            // Only resolve if max rounds reached
            if (!maxRounds.HasValue || currentRounds < maxRounds.Value)
                return ComputePkmnRankings(stats, currentRounds);

            // Fetch pairings to use for head-to-head checks
            var pairings = (await _pairingService.GetAllWhereAsync(p => p.TournamentId == tournamentId)).ToList();

            var active = stats.Where(s => !s.IsDisqualified && !s.IsDropped).ToList();

            // find tie groups by Points and OpWin
            var tieGroups = active
                .GroupBy(s => new { s.Points, Op = Math.Round(s.OpWinPercent, 4) })
                .Where(g => g.Count() > 1)
                .ToList();

            // start with base sorted order (Points then OpWin)
            var baseSorted = active.OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.OpWinPercent)
                .ToList();

            var rnd = _rnd;

            var finalList = new List<PlayerComputedStats>();

            int index = 0;
            while (index < baseSorted.Count)
            {
                var current = baseSorted[index];
                // find group with same keys (points and opwin)
                var group = tieGroups.FirstOrDefault(g => g.Key.Points == current.Points
                    && g.Key.Op == Math.Round(current.OpWinPercent, 4));

                if (group == null)
                {
                    finalList.Add(current);
                    index++;
                    continue;
                }

                var members = group.OrderBy(x => x.TournamentPlayerId).ToList();

                if (members.Count == 2)
                {
                    var ordered = ResolveTwoWayHeadToHead(members[0], members[1], pairings);
                    finalList.AddRange(ordered);
                }
                else
                {
                    // multi-way tie: randomise order as a fallback
                    var order = members.OrderBy(_ => rnd.Next()).ToList();
                    finalList.AddRange(order);
                }

                // remove all group members from baseSorted
                foreach (var m in members)
                {
                    baseSorted.RemoveAll(x => x.TournamentPlayerId == m.TournamentPlayerId);
                }

                // restart scanning from beginning
                index = 0;
            }

            // Now append dropped and disqualified players as before
            var dropped = stats.Where(s => s.IsDropped && !s.IsDisqualified).OrderBy(s => s.TournamentPlayerId).ToList();
            var disq = stats.Where(s => s.IsDisqualified).OrderBy(s => s.TournamentPlayerId).ToList();

            // assign positions sequentially
            int pos = 1;
            foreach (var s in finalList)
            {
                s.Position = pos++;
            }

            foreach (var s in dropped)
            {
                s.Position = pos++;
            }

            foreach (var s in disq)
            {
                s.Position = pos++;
            }

            var combined = new List<PlayerComputedStats>();
            combined.AddRange(finalList);
            combined.AddRange(dropped);
            combined.AddRange(disq);

            return combined;
        }

        // Resolve final MTG ties once tournament ended. Uses match points, Op Win%, Game Win% and Op Game Win% with coin-flip fallback.
        public async Task<List<PlayerComputedStats>> ResolveMtgFinalTiesAsync(List<PlayerComputedStats> stats, int tournamentId, int currentRounds, int? maxRounds)
        {
            // For MTG, resolve final ties after tournament completes
            if (maxRounds.HasValue && currentRounds < maxRounds.Value)
            {
                // Not yet finalised: fallback to ComputeMtgRankings
                return ComputeMtgRankings(stats);
            }

            var pairings = (await _pairingService.GetAllWhereAsync(p => p.TournamentId == tournamentId)).ToList();

            var active = stats.Where(s => !s.IsDisqualified && !s.IsDropped).ToList();

            // Initial sort by Points, OpWin, GameWin, OpGameWin
            var baseSorted = active
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.OpWinPercent)
                .ThenByDescending(x => x.GameWinPercent)
                .ThenByDescending(x => x.OpGameWinPercent)
                .ToList();

            var rnd = _rnd;
            var finalList = new List<PlayerComputedStats>();

            int i = 0;
            while (i < baseSorted.Count)
            {
                var current = baseSorted[i];
                // find any group tied on the same metrics
                var group = baseSorted.Where(x => x.Points == current.Points
                    && Math.Round(x.OpWinPercent, 4) == Math.Round(current.OpWinPercent, 4)
                    && Math.Round(x.GameWinPercent, 4) == Math.Round(current.GameWinPercent, 4)
                    && Math.Round(x.OpGameWinPercent, 4) == Math.Round(current.OpGameWinPercent, 4))
                    .ToList();

                if (group.Count == 1)
                {
                    finalList.Add(current);
                    i++;
                    continue;
                }

                // For two-way tie, try head-to-head
                if (group.Count == 2)
                {
                    var a = group[0];
                    var b = group[1];
                    var ph = pairings.FirstOrDefault(p => p.PairingHasResult
                        && ((p.PairingTp1 == a.TournamentPlayerId
                                && p.PairingTp2 == b.TournamentPlayerId)
                            || (p.PairingTp1 == b.TournamentPlayerId
                                && p.PairingTp2 == a.TournamentPlayerId)));

                    if (ph != null && ph.PairingPlayer1Score.HasValue && ph.PairingPlayer2Score.HasValue)
                    {
                        if (ph.PairingPlayer1Score.Value > ph.PairingPlayer2Score.Value)
                        {
                            if (ph.PairingTp1 == a.TournamentPlayerId)
                            {
                                finalList.Add(a);
                                finalList.Add(b);
                            }
                            else
                            {
                                finalList.Add(b);
                                finalList.Add(a);
                            }
                        }
                        else if (ph.PairingPlayer1Score.Value < ph.PairingPlayer2Score.Value)
                        {
                            if (ph.PairingTp1 == a.TournamentPlayerId)
                            {
                                finalList.Add(b);
                                finalList.Add(a);
                            }
                            else
                            {
                                finalList.Add(a);
                                finalList.Add(b);
                            }
                        }
                        else
                        {
                            var order = new List<PlayerComputedStats> { a, b }.OrderBy(_ => rnd.Next()).ToList();
                            finalList.AddRange(order);
                        }
                    }
                    else
                    {
                        // no head-to-head - randomise coin flip
                        var order = new List<PlayerComputedStats> { a, b }.OrderBy(_ => rnd.Next()).ToList();
                        finalList.AddRange(order);
                    }
                }
                else
                {
                    // multi-way tie: randomise order as a fallback
                    var order = group.OrderBy(_ => rnd.Next()).ToList();
                    finalList.AddRange(order);
                }

                // remove group members from baseSorted
                foreach (var m in group)
                {
                    baseSorted.RemoveAll(x => x.TournamentPlayerId == m.TournamentPlayerId);
                }

                // restart index
                i = 0;
            }

            var dropped = stats.Where(s => s.IsDropped && !s.IsDisqualified).OrderBy(s => s.TournamentPlayerId).ToList();
            var disq = stats.Where(s => s.IsDisqualified).OrderBy(s => s.TournamentPlayerId).ToList();

            int pos = 1;
            foreach (var s in finalList)
            {
                s.Position = pos++;
            }
            foreach (var s in dropped)
            {
                s.Position = pos++;
            }
            foreach (var s in disq)
            {
                s.Position = pos++;
            }

            var combined = new List<PlayerComputedStats>();
            combined.AddRange(finalList);
            combined.AddRange(dropped);
            combined.AddRange(disq);

            return combined;
        }

        // Computes the final rankings for MTG tournaments using MTG-specific tiebreakers
        public List<PlayerComputedStats> ComputeMtgRankings(List<PlayerComputedStats> stats)
        {
            // Separate disqualified/dropped players from active players
            var activeStats = stats
                .Where(s => !s.IsDisqualified && !s.IsDropped)
                .ToList();

            var disqualifiedStats = stats
                .Where(s => s.IsDisqualified)
                .OrderBy(s => s.TournamentPlayerId)
                .ToList();

            var droppedStats = stats
                .Where(s => s.IsDropped && !s.IsDisqualified)
                .OrderBy(s => s.TournamentPlayerId)
                .ToList();

            // Sort active players by MTG tiebreaker rules
            var sorted = activeStats
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.OpWinPercent)
                .ThenByDescending(x => x.GameWinPercent)
                .ThenByDescending(x => x.OpGameWinPercent)
                .ToList();

            // Assign positions to active players
            int position = 1;
            for (int i = 0; i < sorted.Count; i++)
            {
                sorted[i].Position = position++;
            }

            // Dropped players follow active players
            for (int i = 0; i < droppedStats.Count; i++)
            {
                droppedStats[i].Position = position++;
            }

            // Disqualified players follow dropped players
            for (int i = 0; i < disqualifiedStats.Count; i++)
            {
                disqualifiedStats[i].Position = position++;
            }

            // Combine all results in order
            var finalResult = new List<PlayerComputedStats>();
            finalResult.AddRange(sorted);
            finalResult.AddRange(droppedStats);
            finalResult.AddRange(disqualifiedStats);

            return finalResult;
        }
    }
}

