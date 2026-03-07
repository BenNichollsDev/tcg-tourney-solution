using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Pairing
    {
        [Column("pairing_id")]
        public int PairingId { get; private set; }

        [Column("pairing_tp_1")]
        public int PairingTp1 { get; private set; }

        [Column("pairing_tp_2")]
        public int PairingTp2 { get; private set; }

        [Column("pairing_winner")]
        public string? PairingWinner { get; private set; }
    }
}