using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.DTOs
{
    public class TournamentPlayerDTO
    {
        public int TpId { get; set; }

        public int TpTournament { get; set; }

        public int TpPlayer { get; set; }
    }
}
