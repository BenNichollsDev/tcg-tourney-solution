using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.DTOs
{
    public class MatchDTO
    {
        public int MatchId { get; set; }

        public int PairingId { get; set; }

        public int MatchRoundNum { get; set; }

        public bool Player1Winner { get; set; }

        public bool Player2Winner { get; set; }
    }
}
