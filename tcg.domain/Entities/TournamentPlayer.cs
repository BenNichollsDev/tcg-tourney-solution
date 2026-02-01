using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class TournamentPlayer
    {
        public int TpId { get; set; }

        public int TpTournament { get; set; }

        public int TpPlayer { get; set; }
    }
}
