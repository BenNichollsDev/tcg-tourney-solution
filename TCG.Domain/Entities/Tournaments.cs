using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Tournament
    {
        [Column("tournament_id")]
        public int TournamentId { get; private set; }

        [Column("tournament_league")]
        public int? LeagueId { get; private set; }

        [Column("tournament_name")]
        public string TournamentName { get; private set; } = string.Empty;

        [Column("tournament_game")]
        public string TournamentGame { get; private set; } = string.Empty;

        [Column("tournament_format")]
        public string TournamentFormat { get; private set; } = string.Empty;

        [Column("tournament_require_deck")]
        public bool TournamentRequireDeck { get; private set; }

        [Column("tournament_round_num")]
        public int? TournamentRoundNum { get; private set; }

        [Column("tournament_max_round_num")]
        public int? TournamentMaxRoundNum { get; private set; }

        [Column("tournament_description")]
        public string TournamentDescription { get; private set; } = string.Empty;

        [Required(ErrorMessage = "Pairing type is required")]
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

        [Column("tournament_swiss_topcut")]
        public bool TournamentSwissTopcut { get; private set; }

        [Column("tournament_swiss_topcut_num")]
        public int TournamentSwissTopcutNum { get; private set; }

        public League League { get; private set; } = null!;

        public ICollection<TournamentPlayer> TournamentPlayers { get; private set; } = new List<TournamentPlayer>();
        
        public ICollection<Pairing> Pairings { get; private set; } = new List<Pairing>();
    }
}