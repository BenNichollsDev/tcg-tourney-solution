using System;
using System.Collections.Generic;
using System.Text;
using TCG.Application.Interfaces;
using TCG.Application.Dtos;

namespace TCG.Application.Services
{
    public class SetTournament : ISetTournament<TournamentDto>
    {
        public TournamentDto SetTournamentPairing(TournamentDto tournamentDto, string pairingType)
        {
            tournamentDto.TournamentPairing = pairingType;
            return tournamentDto;
        }
    }
}
