using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Tournament
    {
        [Column("tournament_id")]
        public int TournamentId { get; private set; }

        [Column("tournament_league")]
        public int TournamentLeague { get; private set; }

        [Column("tournament_name")]
        public string TournamentName { get; private set; } = string.Empty;

        [Column("tournament_game")]
        public string TournamentGame { get; private set; } = string.Empty;

        [Column("tournament_format")]
        public string TournamentFormat { get; private set; } = string.Empty;

        [Column("tournament_require_deck")]
        public bool TournamentRequireDeck { get; private set; }

        [Column("tournament_round_num")]
        public int TournamentRoundNum { get; private set; }

        [Column("tournament_description")]
        public string TournamentDescription { get; private set; } = string.Empty;

        [Column("tournament_pairing")]
        public string TournamentPairing { get; private set; } = string.Empty;

        [Column("tournament_date")]
        public DateOnly TournamentDate { get; private set; }

        [Column("tournament_time")]
        public TimeOnly TournamentTime { get; private set; }

        [Column("tournament_entry_fee")]
        public decimal TournamentEntryFee { get; private set; }

        [Column("tournament_max_participants")]
        public int TournamentMaxParticipants { get; private set; }
    }
}
