using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Player
    {
        [Column("player_id")]
        public int PlayerId { get; set; }

        [Column("player_first_name")]
        public string PlayerFirstName { get; set; } = string.Empty;

        [Column("player_last_name")]
        public string PlayerLastName { get; set; } = string.Empty;

        [Column("player_email")]
        public string PlayerEmail { get; set; } = string.Empty;

        [Column("player_phone")]
        public string PlayerPhone { get; set; } = string.Empty;

        [Column("player_dob")]
        public DateOnly PlayerDOB { get; set; }

        [Column("player_gender")]
        public string PlayerGender { get; set; } = string.Empty;

        public ICollection<TournamentPlayer>? TournamentPlayers { get; set; } = new List<TournamentPlayer>();
    }
}
