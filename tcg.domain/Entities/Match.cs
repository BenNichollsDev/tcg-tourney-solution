using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class Match
    {
        public int MatchId { get; private set; }

        public int PairingId { get; private set; }

        public int MatchRoundNum { get; private set; }

        public bool Player1Winner { get; private set; }

        public bool Player2Winner { get; private set; }
    }
}
