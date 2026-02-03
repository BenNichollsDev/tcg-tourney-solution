using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Player
    {
        [Column("player_id")]
        public int PlayerId { get; private set; }

        [Column("player_first_name")]
        public string PlayerFirstName { get; private set; } = string.Empty;

        [Column("player_surname")]
        public string PlayerSurname { get; private set; } = string.Empty;

        [Column("player_dob")]
        public DateTime PlayerDob { get; private set; }

        [Column("player_email")]
        public string PlayerEmail { get; private set; } = string.Empty;

        [Column("player_mobile")]
        public long PlayerMobile { get; private set; }
    }
}
