using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Tournament
    {
        [Column("tournament_id")]
        public int TournamentId { get; set; }

        [Column("tournament_league")]
        public int? LeagueId { get; set; }

        [Column("tournament_name")]
        public string TournamentName { get; set; } = string.Empty;

        [Column("tournament_game")]
        public string TournamentGame { get; set; } = string.Empty;

        [Column("tournament_format")]
        public string TournamentFormat { get; set; } = string.Empty;

        [Column("tournament_require_deck")]
        public bool TournamentRequireDeck { get; set; }

        [Column("tournament_round_num")]
        public int? TournamentRoundNum { get; set; }

        [Column("tournament_max_round_num")]
        public int? TournamentMaxRoundNum { get; set; }

        [Column("tournament_description")]
        public string TournamentDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pairing type is required")]
        [Column("tournament_pairing")]
        public string TournamentPairing { get; set; } = string.Empty;

        [Column("tournament_date")]
        public DateOnly TournamentDate { get; set; }

        [Column("tournament_time")]
        public TimeOnly TournamentTime { get; set; }

        [Column("tournament_entry_fee")]
        public decimal TournamentEntryFee { get; set; }

        [Column("tournament_max_participants")]
        public int TournamentMaxParticipants { get; set; }

        [Column("tournament_swiss_topcut")]
        public bool TournamentSwissTopcut { get; set; }

        [Column("tournament_swiss_topcut_num")]
        public int TournamentSwissTopcutNum { get; set; }

        [Column("tournament_in_progress")]
        public bool TournamentInProgress { get; set; }

        public League League { get; set; } = null!;

        public ICollection<TournamentPlayer> TournamentPlayers { get; set; } = new List<TournamentPlayer>();
        
        public ICollection<Pairing> Pairings { get; set; } = new List<Pairing>();
    }
}