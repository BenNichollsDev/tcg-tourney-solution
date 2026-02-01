using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class TournamentPlayer
    {
        public int TpId { get; private set; }

        public int TpTournament { get; private set; }

        public int TpPlayer { get; private set; }

        public int TpPosition { get; private set; }
    }
}
