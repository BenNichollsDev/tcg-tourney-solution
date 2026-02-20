using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.Interfaces
{
    public interface ISetTournament<TournamentDto>
    {
        public TournamentDto SetTournamentPairing(TournamentDto tournamentDto, string pairingType);
    }
}
