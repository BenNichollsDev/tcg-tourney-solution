using System;

namespace TCG.Application.Dtos
{
    public class TournamentPlayerDto
    {
        public int TournamentPlayerId { get; set; }

        public int TournamentId { get; set; }

        public string? PlayerName { get; set; }
        
        public int? PlayerRoundRobinWins { get; set; }

        public int? PlayerRoundRobinScore { get; set; }

        public float? PlayerRoundRobinMatchPoints { get; set; }

        public float? PlayerRoundRobinPoints { get; set; }

        public int? PlayerSwissWins { get; set; }

        public int? PlayerSwissScore { get; set; }

        public float? PlayerSwissMatchPoints { get; set; }

        public float? PlayerSwissPoints { get; set; }
    }
}