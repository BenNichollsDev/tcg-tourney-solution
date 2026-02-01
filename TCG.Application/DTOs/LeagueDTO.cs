using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.DTOs
{
    public class LeagueDTO
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public string LeagueGame { get; set; } = string.Empty;

        public bool LeaguePublic { get; set; }

        public string LeagueDescription { get; set; } = string.Empty;
    }
}
