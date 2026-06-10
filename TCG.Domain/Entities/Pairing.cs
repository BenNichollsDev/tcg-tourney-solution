using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Pairing
    {
        [Column("pairing_id")]
        public int PairingId { get; set; }

        [Column("pairing_tournament_id")]
        public int TournamentId { get; set; }

        [Column("pairing_round_number")]
        public int RoundNumber { get; set; }

        [Column("pairing_tp_1")]
        public int Player1Id { get; set; }

        [Column("pairing_tp_2")]
        public int? Player2Id { get; set; }

        [Column("pairing_tp_1_score")]
        public int? Player1Score { get; set; }

        [Column("pairing_tp_2_score")]
        public int? Player2Score { get; set; }

        [Column("pairing_winner")]
        public int? WinnerId { get; set; }

        [Column("pairing_player_1_game_count")]
        public int? Player1GameCount { get; set; }

        [Column("pairing_player_2_game_count")]
        public int? Player2GameCount { get; set; }

        [Column("pairing_has_result")]
        public bool HasResult { get; set; }

        public Tournament Tournament { get; set; } = null!;

        public TournamentPlayer Player1 { get; set; } = null!;

        public TournamentPlayer? Player2 { get; set; }

        public TournamentPlayer? Winner { get; set; }
    }
}