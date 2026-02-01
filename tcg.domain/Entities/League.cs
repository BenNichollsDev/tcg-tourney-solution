using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class League
    {
        public int LeagueId { get; private set; }

        public string LeagueName { get; private set; } = string.Empty;

        public string LeagueGame { get; private set; } = string.Empty;

        public bool LeaguePublic { get; private set; }

        public string LeagueDescription { get; private set; } = string.Empty;
    }
}
