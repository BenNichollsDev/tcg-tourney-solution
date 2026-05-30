using System;

namespace TCG.Application.Dtos
{
    public class TournamentPlayerDto
    {
        public int TpId { get; set; }

        public int TpTournament { get; set; }

        public string? TpPlayerName { get; set; }

        public int PlayerSwissWins { get; set; }

        public int PlayerSwissScore { get; set; }

        public int PlayerSwissMatchPoints { get; set; }

        public int PlayerSwissPoints { get; set; }
    }
}