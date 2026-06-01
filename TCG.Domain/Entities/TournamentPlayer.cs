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

        [Column("tp_player_swiss_wins")]
        public int? PlayerSwissWins { get; private set; }

        [Column("tp_player_swiss_score")]
        public int? PlayerSwissScore { get; private set; }

        [Column("tp_player_swiss_match_points")]
        public int? PlayerSwissMatchPoints { get; private set; }

        [Column("tp_player_swiss_points")]
        public int? PlayerSwissPoints { get; private set; }

        public Tournament Tournament { get; private set; } = null!;

        // Pairings where this player is Player1
        public ICollection<Pairing> PairingsAsPlayer1 { get; private set; }
            = new List<Pairing>();

        // Pairings where this player is Player2
        public ICollection<Pairing> PairingsAsPlayer2 { get; private set; }
            = new List<Pairing>();
    }
}