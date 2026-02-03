using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class League
    {
        [Column("league_id")]
        public int LeagueId { get; private set; }

        [Column("league_name")]
        public string LeagueName { get; private set; } = string.Empty;

        [Column("league_game")]
        public string LeagueGame { get; private set; } = string.Empty;

        [Column("league_public")]
        public bool LeaguePublic { get; private set; }

        [Column("league_description")]
        public string LeagueDescription { get; private set; } = string.Empty;
    }
}
