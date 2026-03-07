using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class TournamentPlayer
    {
        [Column("tp_id")]
        public int TpId { get; private set; }

        [Column("tp_tournament")]
        public int TpTournament { get; private set; }

        [Column("tp_player_name")]
        public string? TpPlayerName { get; private set; }
        
        public Tournament Tournament { get; private set; } = null!;
    }
}