using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class TournamentPlayer
    {
        [Column("tp_tournament_player_id")]
        public int TournamentPlayerId { get; private set; }

        [Column("tp_tournament_id")]
        public int TournamentId { get; private set; }

        [Column("tp_player_name")]
        public string? PlayerName { get; private set; }
        
        [Column("tp_player_round_robin_wins")]
        public int? PlayerRoundRobinWins { get; private set; }

        [Column("tp_player_round_robin_draws")]
        public int? PlayerRoundRobinDraws { get; private set; }

        [Column("tp_player_round_robin_losses")]
        public int? PlayerRoundRobinLosses { get; private set; }

        [Column("tp_player_round_robin_score")]
        public int? PlayerRoundRobinScore { get; private set; }

        [Column("tp_player_round_robin_match_points")]
        public int? PlayerRoundRobinMatchPoints { get; private set; }

        [Column("tp_player_round_robin_points")]
        public int? PlayerRoundRobinPoints { get; private set; }

        [Column("tp_player_swiss_wins")]
        public int? PlayerSwissWins { get; private set; }

        [Column("tp_player_swiss_draws")]
        public int? PlayerSwissDraws { get; private set; }

        [Column("tp_player_swiss_losses")]
        public int? PlayerSwissLosses { get; private set; }

        [Column("tp_player_swiss_score")]
        public int? PlayerSwissScore { get; private set; }

        [Column("tp_player_swiss_match_points")]
        public int? PlayerSwissMatchPoints { get; private set; }

        [Column("tp_player_swiss_points")]
        public int? PlayerSwissPoints { get; private set; }

        [Column("tp_has_received_bye")]
        public bool HasReceivedBye { get; private set; }

        [Column("tp_games_won")]
        public int? GamesWon { get; private set; }

        [Column("tp_games_played")]
        public int? GamesPlayed { get; private set; }

        public Tournament Tournament { get; private set; } = null!;
        
        public ICollection<Pairing> PairingsAsPlayer1 { get; private set; }
            = new List<Pairing>();
        
        public ICollection<Pairing> PairingsAsPlayer2 { get; private set; }
            = new List<Pairing>();
    }
}