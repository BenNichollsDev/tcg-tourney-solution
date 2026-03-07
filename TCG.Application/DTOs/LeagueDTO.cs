using System;

namespace TCG.Application.Dtos
{
    public class LeagueDto
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public string LeagueGame { get; set; } = string.Empty;

        public string LeagueDescription { get; set; } = string.Empty;
    }
}