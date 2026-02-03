using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class TournamentPlayer
    {
        [Column("tp_id")]
        public int TpId { get; private set; }

        [Column("tp_tournament")]
        public int TpTournament { get; private set; }

        [Column("tp_player")]
        public int TpPlayer { get; private set; }

        [Column("tp_position")]
        public int TpPosition { get; private set; }
    }
}
