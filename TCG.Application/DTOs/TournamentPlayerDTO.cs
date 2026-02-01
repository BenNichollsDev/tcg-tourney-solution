using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.Dtos
{
    public class TournamentPlayerDto
    {
        public int TpId { get; set; }

        public int TpTournament { get; set; }

        public int TpPlayer { get; set; }

        public int TpPosition { get; set; }
    }
}
