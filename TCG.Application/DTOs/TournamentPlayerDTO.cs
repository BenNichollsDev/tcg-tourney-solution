using System;

namespace TCG.Application.Dtos
{
    public class TournamentPlayerDto
    {
        public int TpId { get; set; }

        public int TpTournament { get; set; }

        public string? TpPlayerName { get; set; }
    }
}