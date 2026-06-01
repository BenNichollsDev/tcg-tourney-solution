using System;

namespace TCG.Application.Dtos
{
    public class TournamentPlayerDto
    {
        public int TournamentPlayerId { get; set; }

        public int TournamentId { get; set; }

        public string? PlayerName { get; set; }

        public int? PlayerSwissWins { get; set; }

        public int? PlayerSwissScore { get; set; }

        public int? PlayerSwissMatchPoints { get; set; }

        public int? PlayerSwissPoints { get; set; }
    }
}