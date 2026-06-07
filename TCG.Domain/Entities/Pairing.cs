using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Pairing
    {
        [Column("pairing_id")]
        public int PairingId { get; private set; }

        [Column("pairing_tp_1")]
        public int Player1Id { get; private set; }

        [Column("pairing_tp_2")]
        public int? Player2Id { get; private set; }

        [Column("pairing_tp_1_score")]
        public int? Player1Score { get; private set; }

        [Column("pairing_tp_2_score")]
        public int? Player2Score { get; private set; }

        [Column("pairing_winner")]
        public int? WinnerId { get; private set; }

        // Navigation
        public TournamentPlayer Player1 { get; private set; } = null!;
        
        public TournamentPlayer? Player2 { get; private set; }
        
        public TournamentPlayer? Winner { get; private set; }
    }
}