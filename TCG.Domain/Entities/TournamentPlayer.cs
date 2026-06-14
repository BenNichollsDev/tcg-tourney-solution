using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class TournamentPlayer
    {
        [Column("tp_tournament_player_id")]
        public int TournamentPlayerId { get; set; }

        [Column("tp_tournament_id")]
        public int TournamentId { get; set; }

        [Column("tp_player_name")]
        public string? PlayerName { get; set; }
        
        [Column("tp_player_round_robin_wins")]
        public int? PlayerRoundRobinWins { get; set; }

        [Column("tp_player_round_robin_draws")]
        public int? PlayerRoundRobinDraws { get; set; }

        [Column("tp_player_round_robin_losses")]
        public int? PlayerRoundRobinLosses { get; set; }

        [Column("tp_player_round_robin_score")]
        public int? PlayerRoundRobinScore { get; set; }

        [Column("tp_player_round_robin_match_points")]
        public int? PlayerRoundRobinMatchPoints { get; set; }

        [Column("tp_player_round_robin_points")]
        public int? PlayerRoundRobinPoints { get; set; }

        [Column("tp_player_swiss_wins")]
        public int? PlayerSwissWins { get; set; }

        [Column("tp_player_swiss_draws")]
        public int? PlayerSwissDraws { get; set; }

        [Column("tp_player_swiss_losses")]
        public int? PlayerSwissLosses { get; set; }

        [Column("tp_player_swiss_score")]
        public int? PlayerSwissScore { get; set; }

        [Column("tp_player_swiss_match_points")]
        public int? PlayerSwissMatchPoints { get; set; }

        [Column("tp_player_swiss_points")]
        public int? PlayerSwissPoints { get; set; }

        [Column("tp_byes")]
        public int? PlayerBye { get; set; }

        [Column("tp_games_played")]
        public int? GamesPlayed { get; set; }

        [Column("tp_matches_played")]
        public int? MatchesPlayed { get; set; }

        [Column("tp_disqualified")]
        public bool TpDisqualified { get; set; }

        [Column("tp_dropped")]
        public bool TpDropped { get; set; }

        [Column("tp_position")]
        public int? TpPosition { get; set; }

        public Tournament Tournament { get; set; } = null!;
        
        public ICollection<Pairing> PairingsAsPlayer1 { get; set; }
            = new List<Pairing>();
        
        public ICollection<Pairing> PairingsAsPlayer2 { get; set; }
            = new List<Pairing>();
    }
}