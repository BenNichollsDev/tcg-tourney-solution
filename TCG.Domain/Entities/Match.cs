using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Match
    {
        [Column("match_id")]
        public int MatchId { get; private set; }

        [Column("pairing_id")]
        public int PairingId { get; private set; }

        [Column("match_round_num")]
        public int MatchRoundNum { get; private set; }

        [Column("player1_winner")]
        public bool Player1Winner { get; private set; }

        [Column("player2_winner")]
        public bool Player2Winner { get; private set; }
    }
}
