using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class League
    {
        [Column("league_id")]
        public int LeagueId { get; set; }

        [Column("league_name")]
        public string LeagueName { get; set; } = string.Empty;

        [Column("league_game")]
        public string LeagueGame { get; set; } = string.Empty;

        [Column("league_description")]
        public string LeagueDescription { get; set; } = string.Empty;

        public ICollection<Tournament> Tournaments { get; set; }
            = new List<Tournament>();
    }
}